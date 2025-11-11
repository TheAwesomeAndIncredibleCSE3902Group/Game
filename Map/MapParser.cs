using System;
using System.Collections;
using System.Collections.Generic;
using AwesomeRPG.Characters;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using static AwesomeRPG.Util;


namespace AwesomeRPG.Map;

public class MapParser
{
    private MapParser()
    {

    }

    private static MapParser instance = new MapParser();


    public static MapParser Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// Generates and returns a RoomMap from an xml file
    /// </summary>
    /// <param name="content">The content manager</param>
    /// <param name="filename">The name of the level.xml file</param>
    /// <returns>A RoomMap with tiles, walls, characters, and pickups</returns>
    public static RoomMap RoomMapFromXML(ContentManager content, string filename)
    {
        string filePath = Path.Combine(content.RootDirectory, filename);

        using Stream stream = TitleContainer.OpenStream(filePath);
        using XmlReader reader = XmlReader.Create(stream);
        XDocument doc = XDocument.Load(reader);
        XElement mapElement = doc.Root;

        // tilemap contains all background information
        XElement tilemapElement = mapElement.Element("Tilemap");

        // tileset contains tile image info
        XElement tilesetElement = tilemapElement.Element("Tileset");
        int tileWidth = int.Parse(tilesetElement.Attribute("tileWidth").Value);
        int tileHeight = int.Parse(tilesetElement.Attribute("tileHeight").Value);
        string contentPath = tilesetElement.Value;

        // Load the texture 2d containing tileset
        Texture2D tileTexture = content.Load<Texture2D>(contentPath);

        // Create the tileset using the texture region
        TileSet tileset = new(tileTexture, tileWidth, tileHeight);

        // The <Tiles> element contains <Row></Row>s of strings where <Row>
        // contains space seperated tile texture ids. ids beginning with ! are collideable ? is entrance
        XElement tilesElement = tilemapElement.Element("Tiles");

        int columns = int.Parse(tilesElement.Attribute("columns").Value);
        int rows = int.Parse(tilesElement.Attribute("rows").Value);
        Tilemap tilemap = new(tileset, columns, rows);

        // 2d list where 1 indictates wall, 2 is entrance, and 0 is no collision.
        // doesn't use booleans so it can later be optimised to generate larger collision rectangles
        List<List<int>> collisionMatrix = ParseLayout(tilesElement, columns, rows, tilemap);

        // the map to return
        RoomMap map = new(tilemap);

        GenerateCollision(map, collisionMatrix, (int)(tileWidth * GlobalScale), (int)(tileHeight * GlobalScale));

        IEnumerable entityElements = mapElement.Element("Entities").Elements("Entity");
        foreach (XElement entity in entityElements)
        {
            GenerateCharacter(map, entity);
        }

        // Generate Pickups
        IEnumerable pickupElements = mapElement.Element("Pickups").Elements("Pickup");
        foreach (XElement pickup in pickupElements)
        {
            GeneratePickup(map, pickup);
        }
        return map;
    }

    /// <summary>
    /// Parses the tiles and collision layout from the <Tiles> XML element
    /// </summary>
    /// <param name="tilesElement">The <Tiles> XML element from a room map</param>
    /// <param name="columns">The number of columns</param>
    /// <param name="rows">The number of rows</param>
    /// <param name="tilemap">The tilemap to assign background tiles to</param>
    /// <returns>A 2D matrix where 0 is no collision, 1 is wall, and 2 is </returns>
    private static List<List<int>> ParseLayout(XElement tilesElement, int columns, int rows, Tilemap tilemap)
    {
        List<List<int>> collisionMatrix = new(columns);

        IEnumerable rowElements = tilesElement.Elements("Row");
        int i = 0;
        foreach (XElement row in rowElements)
        {
            collisionMatrix.Add(new List<int>(rows));
            string[] rowTileIDs = row.Value.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            for (int column = 0; column < rowTileIDs.Length; column++)
            {
                string tileInfo = rowTileIDs[column];
                if (tileInfo.StartsWith('!'))
                {
                    collisionMatrix[i].Add(1);
                }
                else if (tileInfo.StartsWith('?'))
                {
                    collisionMatrix[i].Add(2);
                }
                else
                {
                    collisionMatrix[i].Add(0);
                }
                int tilesetIndex = int.Parse(tileInfo.Trim('!').Trim('?'));

                tilemap.SetTile(column, i, tilesetIndex);

            }
            i++;
        }
        return collisionMatrix;
    }


    /// <summary>
    /// Adds Walls and Entrances to a map's collision list based on a collision matrix.
    /// </summary>
    /// <param name="map">The room map to add collision to</param>
    /// <param name="collisionMatrix">Matrix where 1=collision, 2=entrance</param>
    /// <param name="width">Collision hitbox width</param>
    /// <param name="height">Collision hitbox height</param>
    private static void GenerateCollision(RoomMap map, List<List<int>> collisionMatrix, int width, int height)
    {
        for (int i = 0; i < collisionMatrix.Count; i++)
        {
            for (int j = 0; j < collisionMatrix[i].Count; j++)
            {
                Vector2 position = new(j * width, i * height);
                if (collisionMatrix[i][j] == 1)
                {
                    map._nonMovingCollisionObjects.Add(new Wall(position, width, height));
                }
                else if (collisionMatrix[i][j] == 2)
                {
                    map._nonMovingCollisionObjects.Add(new Entrance(position, width, height));
                }
            }
        }
    }


    /// <summary>
    /// Generates a character from an <Entity> XML tag, and adds it to the map.
    /// </summary>
    /// <param name="map">The map to add the character to</param>
    /// <param name="entityTag"><Entity> XML tag</param>
    private static void GenerateCharacter(RoomMap map, XElement entityTag)
    {
        string type = entityTag.Value.Trim().ToLower();
        Vector2 position = new(int.Parse(entityTag.Attribute("x").Value), int.Parse(entityTag.Attribute("y").Value));
        Cardinal facing;
        // try catch, because some entities don't need facing
        try
        {
            {
                facing = entityTag.Attribute("facing").Value.Trim().ToLower() switch
                {
                    "up" => Cardinal.up,
                    "down" => Cardinal.down,
                    "left" => Cardinal.left,
                    _ => Cardinal.right,
                };
            }
        }
        catch
        {
            facing = Cardinal.right;
        }

        IPathingScheme pathing;
        // try catch, because entities aren't required to have pathing
        try
        {
            {
                pathing = entityTag.Attribute("pathing").Value.Trim().ToLower() switch
                {
                    "line" => new LinePathing(facing),
                    _ => new RandomWalkPathing(facing),
                };
            }
        }
        catch
        {
            pathing = new RandomWalkPathing(facing);
        }

        ICharacter character;
        switch (type)
        {
            case "moblin":
                character = new CharacterEnemyMoblin(position, facing);
                break;
            case "armos":
                character = new CharacterEnemyArmos(position, facing);
                break;
            case "lynel":
                character = new CharacterEnemyLynel(position, facing);
                break;
            case "kris":
                character = new CharacterKris();
                break;
            default:
                Console.WriteLine("Entity type not supported: " + type);
                character = new CharacterKris(); //Arbitrary
                break;
        }
        character.Pathing = pathing;

        map.Characters.Add(character);
        //Reflection used here because as of Sprint3 Kris does not have collision. It will be revamped soon.
        CharacterEnemyBase enemyBase = character as CharacterEnemyBase;
        if (enemyBase is not null)
        {
            map._movingCollisionObjects.Add(enemyBase);
        }
        return;
    }


    /// <summary>
    /// Generates a pickup and adds it to the map
    /// </summary>
    /// <param name="map">Map to add pickup to</param>
    /// <param name="pickup"><Pickup> XML element</param>
    private static void GeneratePickup(RoomMap map, XElement pickup)
    {
        string type = pickup.Value.Trim().ToLower();
        Vector2 position = new(int.Parse(pickup.Attribute("x").Value), int.Parse(pickup.Attribute("y").Value));

        Pickup pickupToAdd;
        switch (type)
        {
            case "potion":
                pickupToAdd = new Potion(map);
                pickupToAdd.Position = position;
                break;
            default:
                Console.WriteLine("Pickup type not supported: " + type);
                pickupToAdd = new Potion(map); //Arbitrary
                break;
        }
        map.Pickups.Add(pickupToAdd);
        map._nonMovingCollisionObjects.Add(pickupToAdd);
        
        return;
    }

}