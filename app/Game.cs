using System.Globalization;
using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;

class Game
{
    int ScreenWidth;
    int ScreenHeight;

    const float BlockSize = 112.0f;
    int GridSizeX;
    int GridSizeY;

    int MapWidth;
    int MapHeight;

    TileType[,] tiles;
    Morio morio;
    Texture2D blocksTex;
    Rectangle[] blockTextureSourceRects = {
        new(0.0f, 0.0f, 16.0f, 16.0f),
        new(17.0f, 0.0f, 16.0f, 16.0f),
        new(34.0f, 0.0f, 16.0f, 16.0f),
        new(0.0f, 17.0f, 16.0f, 16.0f),
        new(17.0f, 17.0f, 16.0f, 16.0f),
        new(34.0f, 17.0f, 16.0f, 16.0f),
        new(0.0f, 34.0f, 16.0f, 16.0f),
        new(17.0f, 34.0f, 16.0f, 16.0f),
        new(34.0f, 34.0f, 16.0f, 16.0f),
    };

    public Game(int _screenWidth, int _screenHeight)
    {
        ScreenWidth = _screenWidth;
        ScreenHeight = _screenHeight;

        GridSizeX = 34;
        GridSizeY = 10;

        MapWidth = (int)(GridSizeX * BlockSize);
        MapHeight = (int)(GridSizeY * BlockSize);

        // Map
        TileType[,] _tiles = {
            { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
            { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
            { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
            { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
            { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
            { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
            { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
            { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
            { TileType.Empty, TileType.Empty, TileType.Grass_TL, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TR, TileType.Empty, TileType.Empty, },
            { TileType.Empty, TileType.Empty, TileType.Grass_ML, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MR, TileType.Empty, TileType.Empty, },
        };

        tiles = _tiles;
        morio = new Morio();
        blocksTex = LoadTexture("assets/tiles.png");

    }

    public void Update()
    {
        morio.Update();
    }

    public void Render()
    {
        Vector2 origin = new(0.0f, 0.0f);
        for (int j = 0; j < GridSizeY; j++)
        {
            for (int i = 0; i < GridSizeX; i++)
            {
                if (tiles[j, i].GetHashCode() == -1)
                {
                    continue;
                }

                Vector2 pos = new(i * BlockSize - morio.x, (j - 0.2f) * BlockSize);
                Rectangle src = blockTextureSourceRects[tiles[j, i].GetHashCode()];
                Rectangle dest = new(pos, BlockSize, BlockSize);

                DrawTexturePro(blocksTex, src, dest, origin, 0.0f, Color.RayWhite);
            }
        }
        morio.Render();

        DrawLine(0 - (int)morio.x, 0, 0 - (int)morio.x, 1024, Color.Red);
        DrawLine(34 * (int)BlockSize - (int)morio.x, 0, 34 * (int)BlockSize - (int)morio.x + 5, 1024, Color.Red);
    }
}
