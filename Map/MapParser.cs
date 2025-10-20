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

    private static MapParser instance = new MapParser();

    public static MapParser Instance
    {
        get
        {
            return instance;
        }
    }

    private static List<ICharacter> _characterSet;

    public static void SetupParser(List<ICharacter> characters)
    {
        _characterSet = characters;
    }

    public enum EntityType { Player, Enemy, Item, Block }

    public RoomMap RoomMapFromXML(ContentManager content, string filename, Vector2 scale)
    {
        string filePath = Path.Combine(content.RootDirectory, filename);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement mapElement = doc.Root;

                // The <Tileset> element contains the information about the tileset
                // used by the tilemap.
                //
                // Example
                // <Tilemap>
                //      <Tileset region="100 100" tileWidth="10" tileHeight="10">contentPath</Tileset>
                //      <Tiles>...</Tiles>
                // </Tilemap>
                XElement tilemapElement = mapElement.Element("Tilemap");
                XElement tilesetElement = tilemapElement.Element("Tileset");
                int width = int.Parse(tilesetElement.Attribute("width").Value);
                int height = int.Parse(tilesetElement.Attribute("height").Value);

                int tileWidth = int.Parse(tilesetElement.Attribute("tileWidth").Value);
                int tileHeight = int.Parse(tilesetElement.Attribute("tileHeight").Value);
                string contentPath = tilesetElement.Value;

                // Load the texture 2d at the content path
                Texture2D texture = content.Load<Texture2D>(contentPath); ;

                // Create the tileset using the texture region
                TileSet tileset = new TileSet(texture, tileWidth, tileHeight);

                // The <Tiles> element contains <Row></Row>s of strings where <Row>
                // contains space seperated tile texture ids
                // Example:
                // <Tiles rows="4" columns="4">
                //      <Row>00 01 01 02</Row>
                //      <Row>03 04 04 05</Row>
                //      <Row>03 04 04 05</Row>
                //      <Row>06 07 07 08</Row>
                // </Tiles>
                XElement tilesElement = tilemapElement.Element("Tiles");

                Tilemap tilemap = new Tilemap(tileset,
                                        int.Parse(tilesElement.Attribute("columns").Value),
                                        int.Parse(tilesElement.Attribute("rows").Value));
                tilemap.Scale = scale;

                // set up tilemap using data from rows
                IEnumerable rowElements = tilesElement.Elements("Row");
                int i = 0;
                foreach (XElement row in rowElements)
                {

                    string[] rowTileIDs = row.Value.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    for (int column = 0; column < rowTileIDs.Length; column++)
                    {
                        int tilesetIndex = int.Parse(rowTileIDs[column]);

                        Tile region = tileset.GetTile(tilesetIndex);

                        tilemap.SetTile(column, i, tilesetIndex);
                    }
                    i++;
                }

                RoomMap map = new RoomMap(tilemap);

                IEnumerable entityElements = mapElement.Element("Entities").Elements("Entity");
                int j = 0;
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
                    // try catch, because some entities don't need pathing
                    try {
                        {
                            switch (entity.Attribute("pathing").Value.Trim().ToLower())
                            {
                                case "line":
                                    pathing = new LinePathing(facing);
                                    break;
                                case "random":
                                default:
                                    pathing = new RandomWalkPathing(facing);
                                    break;
                            }
                        }
                    } catch
                    {
                        pathing = new RandomWalkPathing(facing);
                    }

                    switch (type)
                    {
                        case "moblin":
                            CharacterEnemyMoblin moblin = new CharacterEnemyMoblin(position, facing);
                            moblin.Pathing = pathing;
                            _characterSet.Add(moblin);
                            break;
                        case "armos":
                            CharacterEnemyArmos armos = new CharacterEnemyArmos(position, facing);
                            armos.Pathing = pathing;
                            _characterSet.Add(armos);
                            break;
                        case "lynel":
                            CharacterEnemyLynel lynel = new CharacterEnemyLynel(position, facing);
                            lynel.Pathing = pathing;
                            _characterSet.Add(lynel);
                            break;
                        case "kris":
                            CharacterKris kris = new CharacterKris();
                            _characterSet.Add(kris);
                            break;
                        default:
                            Console.WriteLine("Type not supported: " + type);
                            break;
                    }
                    
                }

                return map;

            }
        }
    }


}