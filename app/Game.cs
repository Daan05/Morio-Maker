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
        new(48.0f, 0.0f, 16.0f, 16.0f)
    };

    public Game(int _screenWidth, int _screenHeight)
    {
        ScreenWidth = _screenWidth;
        ScreenHeight = _screenHeight;

        GridSizeX = 20;
        GridSizeY = 10;

        MapWidth = (int)(GridSizeX * BlockSize);
        MapHeight = (int)(GridSizeY * BlockSize);

        // Map
        TileType[,] _tiles = {
            { TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, },
            { TileType.Block, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Block, },
            { TileType.Block, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Block, },
            { TileType.Block, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Block, },
            { TileType.Block, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Block, },
            { TileType.Block, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Block, },
            { TileType.Block, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Block, },
            { TileType.Block, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Block, },
            { TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, },
            { TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, TileType.Block, },
        };

        tiles = _tiles;
        morio = new Morio();
        blocksTex = LoadTexture("assets/blocks.png");

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
    }
}
