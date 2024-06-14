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
    readonly Rectangle[] blockTextureSourceRects = {
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

        GridSizeX = (int)((float)ScreenWidth / BlockSize); //16
        GridSizeY = (int)((float)ScreenHeight / BlockSize); //10

        if ((float)ScreenHeight % BlockSize != 0 || (float)ScreenWidth % BlockSize != 0)
        {
            System.Console.WriteLine("WARNING: screensize doesn't match blocksize");
        }

        // System.Console.WriteLine(ScreenHeight + " / " + BlockSize + " = " + GridSizeY);

        MapWidth = (int)(GridSizeX * BlockSize);
        MapHeight = (int)(GridSizeY * BlockSize);
        // System.Console.WriteLine(MapHeight);

        TileType[,] _tiles = new TileType[GridSizeY, GridSizeX * 3];
        System.Console.WriteLine(_tiles.GetLength(1));

        for (int j = 0; j < _tiles.GetLength(0); j++)
        {
            for (int i = 0; i < _tiles.GetLength(1); i++)
            {
                _tiles[j, i] = TileType.Empty;
                if (j == GridSizeY - 1)
                {
                    _tiles[j, i] = TileType.Grass_TM;
                }
            }

        }
        // Map
        // TileType[,] _tiles = {
        //     { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
        //     { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
        //     { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
        //     { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
        //     { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
        //     { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
        //     { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
        //     { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, },
        //     { TileType.Empty, TileType.Empty, TileType.Grass_TL, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TM, TileType.Grass_TR, TileType.Empty, TileType.Empty, },
        //     { TileType.Empty, TileType.Empty, TileType.Grass_ML, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MM, TileType.Grass_MR, TileType.Empty, TileType.Empty, },
        // };

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
        for (int j = 0; j < tiles.GetLength(0); j++)
        {
            for (int i = 0; i < tiles.GetLength(1); i++)
            {
                if (tiles[j, i].GetHashCode() == -1)
                {
                    continue;
                }

                // why (j -0.2f) instead of just j?
                Vector2 pos = new(i * BlockSize - morio.x, j * BlockSize);
                Rectangle src = blockTextureSourceRects[tiles[j, i].GetHashCode()];
                Rectangle dest = new(pos, BlockSize, BlockSize);

                DrawTexturePro(blocksTex, src, dest, origin, 0.0f, Color.RayWhite);
            }
        }
        morio.Render();

        DrawLine(0 - (int)morio.x, 0, 0 - (int)morio.x, 1024, Color.Red);
        DrawLine(34 * (int)BlockSize - (int)morio.x, 0, 34 * (int)BlockSize - (int)morio.x + 5, 1024, Color.Red);

        // Draw gridlines for debugging purposes, do not remove
        if (true)
        {
            for (int i = 0; i < GridSizeY; i++)
            {
                int LineY = i * (int)BlockSize;
                // System.Console.WriteLine(LineY);
                DrawLine(0, LineY, ScreenWidth, LineY, Color.Black);

            }
            for (int i = 0; i < GridSizeX * 4; i++)
            {
                int LineX = (int)((float)((i - GridSizeX * 2) * BlockSize) - morio.x);
                DrawLine(LineX, 0, LineX, ScreenHeight, Color.Black);

            }
        }

    }
}
