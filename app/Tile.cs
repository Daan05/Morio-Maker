using System.Numerics;
using Raylib_cs;

class Tile
{
    static Vector2 texSize = new(16f, 16f);

    public static Rectangle[] blockTexSourceRects = {
        new(0.0f, 0.0f, texSize),
        new(17.0f, 0.0f, texSize),
        new(34.0f, 0.0f, texSize),
        new(0.0f, 17.0f, texSize),
        new(17.0f, 17.0f, texSize),
        new(34.0f, 17.0f, texSize),
        new(0.0f, 34.0f, texSize),
        new(17.0f, 34.0f, texSize),
        new(34.0f, 34.0f, texSize),
    };

    public TileType type = TileType.Empty;

    public Tile(TileType _type)
    {
        type = _type;

    }
}

enum TileType
{
    Empty = -1,
    Grass_TL = 0,
    Grass_TM = 1,
    Grass_TR = 2,
    Grass_ML = 3,
    Grass_MM = 4,
    Grass_MR = 5,
    Grass_BL = 6,
    Grass_BM = 7,
    Grass_BR = 8,
    Brick = 9
}