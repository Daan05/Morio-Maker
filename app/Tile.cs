class Tile
{
    public TileType type = TileType.Empty;

    public Tile(TileType _type)
    {
        type = _type;

    }
}

enum TileType
{
    Empty = -1,
    Block = 0,
}