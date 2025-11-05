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
using System.Linq;
using AwesomeRPG.Collision;
using AwesomeRPG.BattleMechanics;


namespace AwesomeRPG.Map;

public class MapParser
{
    private MapParser()
    {

    }

    private static MapParser instance = new MapParser();

    private RoomAtlas roomAtlas;
    private Game1 myGame;

    public static MapParser Instance
    {
        get
        {
            return instance;
        }
    }

    public void LoadParser(Game1 game, RoomAtlas atlas)
    {
        myGame = game;
        roomAtlas = atlas;
    }

    public RoomMap RoomMapFromXML(ContentManager content, string filename)
    {
        string filePath = Path.Combine(content.RootDirectory, filename);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement mapElement = doc.Root;

                // tilemap contains all background information
                XElement tilemapElement = mapElement.Element("Tilemap");
                // tileset contains tile image info
                XElement tilesetElement = tilemapElement.Element("Tileset");
                int width = int.Parse(tilesetElement.Attribute("width").Value);
                int height = int.Parse(tilesetElement.Attribute("height").Value);

                int tileWidth = int.Parse(tilesetElement.Attribute("tileWidth").Value);
                int tileHeight = int.Parse(tilesetElement.Attribute("tileHeight").Value);
                string contentPath = tilesetElement.Value;

                // Load the texture 2d containing tileset
                Texture2D tileTexture = content.Load<Texture2D>(contentPath); ;

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
                List<List<int>> collisionMatrix = new(columns);

                // set up tilemap using data from rows
                
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

                        Tile region = tileset.GetTile(tilesetIndex);

                        tilemap.SetTile(column, i, tilesetIndex);

                    }
                    i++;
                }

                // the map to return
                RoomMap map = new RoomMap(tilemap);

                // handle collision
                for (i = 0; i < collisionMatrix.Count; i++)
                {
                    for (int j = 0; j < collisionMatrix[i].Count; j++)
                    {
                        if (collisionMatrix[i][j] == 1)
                        {
                            map._nonMovingCollisionObjects.Add(new Wall(new Vector2(j * tileWidth * Util.GlobalScale, i * tileHeight * Util.GlobalScale), (int)(tileWidth * Util.GlobalScale), (int)(tileHeight * Util.GlobalScale)));
                        }
                        else if (collisionMatrix[i][j] == 2)
                        {
                            map._nonMovingCollisionObjects.Add(new Entrance(new Vector2(j * tileWidth * Util.GlobalScale, i * tileHeight * Util.GlobalScale), (int)(tileWidth * Util.GlobalScale), (int)(tileHeight * Util.GlobalScale)));
                        }
                    }
                }

                // generate entities

                IEnumerable entityElements = mapElement.Element("Entities").Elements("Entity");
                foreach (XElement entity in entityElements)
                {
                    string type = entity.Value.Trim().ToLower();

                    Vector2 position = new Vector2(int.Parse(entity.Attribute("x").Value), int.Parse(entity.Attribute("x").Value));

                    Cardinal facing;
                    // try catch, because some entities don't need facing
                    try
                    {
                        {
                            switch (entity.Attribute("facing").Value.Trim().ToLower())
                            {
                                case "up":
                                    facing = Cardinal.up;
                                    break;
                                case "down":
                                    facing = Cardinal.down;
                                    break;
                                case "left":
                                    facing = Cardinal.left;
                                    break;
                                default:
                                    facing = Cardinal.right;
                                    break;
                            }
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
                            switch (entity.Attribute("pathing").Value.Trim().ToLower())
                            {
                                case "line":
                                    pathing = new LinePathing(facing);
                                    break;
                                case "random":
                                // intentional spillover into default
                                default:
                                    pathing = new RandomWalkPathing(facing);
                                    break;
                            }
                        }
                    }
                    catch
                    {
                            pathing = new RandomWalkPathing(facing);
                        }

                    ICharacter enemy;
                    switch (type)
                    {
                        case "moblin":
                            enemy = new CharacterEnemyMoblin(position, facing);
                            break;
                        case "armos":
                            enemy = new CharacterEnemyMoblin(position, facing);
                            break;
                        case "lynel":
                            enemy = new CharacterEnemyLynel(position, facing);
                            break;
                        case "kris":
                            enemy = new CharacterKris();
                            break;
                        default:
                            Console.WriteLine("Entity type not supported: " + type);
                            enemy = new CharacterKris(); //Arbitrary
                            break;
                    }
                    enemy.Pathing = pathing;
                    map.Characters.Add(enemy);

                    //Reflection used here because as of Sprint3 Kris does not have collision. It will be revamped soon.
                    CharacterEnemyBase enemyBase = enemy as CharacterEnemyBase;
                    if (enemyBase is not null)
                        map._movingCollisionObjects.Add(enemyBase);
                }

                // generate pickups
                
                IEnumerable pickupElements = mapElement.Element("Pickups").Elements("Pickup");
                foreach (XElement pickup in pickupElements)
                {
                    string type = pickup.Value.Trim().ToLower();
                    Vector2 position = new Vector2(int.Parse(pickup.Attribute("x").Value), int.Parse(pickup.Attribute("y").Value));

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
                }

                return map;

            }
        }
    }


}