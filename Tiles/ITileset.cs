using System;

namespace Sprint0.Tiles;

public interface ITileset
{
public int TileWidth { get; }
public int TileHeight { get; }
public int Columns { get; }
public int Rows { get; }
public int Count { get; }
}
