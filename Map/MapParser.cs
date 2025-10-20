using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using static AwesomeRPG.Util;


namespace AwesomeRPG.Map;

public static class MapParser
{
    public enum EntityType { Player, Enemy, Item, Block }

    public static RoomMap RoomMapFromXML(ContentManager content, string filename, Cardinal? enteredfrom)
    {
        string filePath = Path.Combine(content.RootDirectory, filename);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement root = doc.Root;

                // The <Tileset> element contains the information about the tileset
                // used by the tilemap.
                //
                // Example
                // <Tilemap>
                //      <Tileset region="100 100" tileWidth="10" tileHeight="10">contentPath</Tileset>
                //      <Tiles>...</Tiles>
                // </Tilemap>
                XElement tilemapElement = root.Element("Tilemap");
                XElement tilesetElement = tilemapElement.Element("Tileset");

                string regionAttribute = tilesetElement.Attribute("region").Value;
                string[] split = regionAttribute.Split(" ", StringSplitOptions.RemoveEmptyEntries);
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
                XElement tilesElement = root.Element("Tiles");

                Tilemap tilemap = new Tilemap(tileset,
                                        int.Parse(tilesElement.Attribute("columns").Value),
                                        int.Parse(tilesElement.Attribute("rows").Value));

                // set up tilemap using data from rows
                XElement[] rowElements = (XElement[])tilesElement.Elements("Row");
                for (int row = 0; row < rowElements.Length; row++)
                {

                    string[] rowTileIDs = rowElements[row].Value.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    for (int column = 0; column < rowTileIDs.Length; column++)
                    {
                        int tilesetIndex = int.Parse(rowTileIDs[column]);

                        Tile region = tileset.GetTile(tilesetIndex);

                        tilemap.SetTile(column, row, tilesetIndex);
                    }
                }
                
                return new RoomMap(tilemap);
                
            }
        }
    }


}