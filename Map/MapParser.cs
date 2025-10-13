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
                // <Tileset region="100 100" tileWidth="10" tileHeight="10">contentPath</Tileset>
                //
                // The region attribute represents the width and height
                // components of the boundary for the texture region within the
                // texture at the contentPath specified.
                //
                // the tileWidth and tileHeight attributes specify the width and
                // height of each tile in the tileset.
                //
                // the contentPath value is the contentPath to the texture to
                // load that contains the tileset
                XElement tilesetElement = root.Element("Tileset");

                string regionAttribute = tilesetElement.Attribute("region").Value;
                string[] split = regionAttribute.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                int width = int.Parse(split[0]);
                int height = int.Parse(split[1]);

                int tileWidth = int.Parse(tilesetElement.Attribute("tileWidth").Value);
                int tileHeight = int.Parse(tilesetElement.Attribute("tileHeight").Value);
                string contentPath = tilesetElement.Value;

                // Load the texture 2d at the content path
                Texture2D texture = content.Load<Texture2D>(contentPath); ;

                // Create the tileset using the texture region
                TileSet tileset = new TileSet(texture, tileWidth, tileHeight);

                // The <Tiles> element contains lines of strings where each line
                // represents a row in the tilemap.  Each line is a space
                // separated string where each element represents a column in that
                // row.  The value of the column is the id of the tile in the
                // tileset to draw for that location.
                //
                // Example:
                // <Tiles>
                //      00 01 01 02
                //      03 04 04 05
                //      03 04 04 05
                //      06 07 07 08
                // </Tiles>
                XElement tilesElement = root.Element("Tiles");

                // Split the value of the tiles data into rows by splitting on
                // the new line character
                string[] rows = tilesElement.Value.Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries);

                // Split the value of the first row to determine the total number of columns
                int columnCount = rows[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;

                // Create the tilemap
                Tilemap tilemap = new Tilemap(tileset, columnCount, rows.Length);

                // Process each row
                for (int row = 0; row < rows.Length; row++)
                {
                    // Split the row into individual columns
                    string[] columns = rows[row].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    // Process each column of the current row
                    for (int column = 0; column < columnCount; column++)
                    {
                        // Get the tileset index for this location
                        int tilesetIndex = int.Parse(columns[column]);

                        // Get the texture region of that tile from the tileset
                        Tile region = tileset.GetTile(tilesetIndex);

                        // Add that region to the tilemap at the row and column location
                        tilemap.SetTile(column, row, tilesetIndex);
                    }
                }

            }
        }
        return new RoomMap();
    }


}