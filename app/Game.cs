using System.Globalization;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Constants;

class Game
{
    int GridSizeX;
    int GridSizeY;

    int MapWidth;
    int MapHeight;

    TileType[,] tiles;
    Morio morio;
    Texture2D blocksTex;

    public Game()
    {
        GridSizeX = (int)((float)WindowWidth / BlockSize); 
        GridSizeY = (int)((float)WindowHeight / BlockSize); 

        if ((float)WindowHeight % BlockSize != 0 || (float)WindowWidth % BlockSize != 0)
        {
            Console.WriteLine("WARNING: screensize doesn't match blocksize");
        }
        // Console.WriteLine(ScreenHeight + " / " + BlockSize + " = " + GridSizeY);

        MapWidth = (int)(GridSizeX * BlockSize);
        MapHeight = (int)(GridSizeY * BlockSize);
        // Console.WriteLine(MapHeight);

        TileType[,] _tiles = new TileType[GridSizeY, GridSizeX * 3];

        for (int j = 0; j < _tiles.GetLength(0); j++)
        {
            for (int i = 0; i < _tiles.GetLength(1); i++)
            {
                _tiles[j, i] = TileType.Empty;
                if (j == GridSizeY - 1)
                {
                    _tiles[j, i] = TileType.Grass_MM;
                }
                else if (j == GridSizeY - 2)
                {
                    _tiles[j, i] = TileType.Grass_TM;
                }
            }

        }

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

                Vector2 pos = new(i * BlockSize - morio.x, j * BlockSize);
                Rectangle src = Tile.blockTexSourceRects[tiles[j, i].GetHashCode()];
                Rectangle dest = new(pos, BlockSize + 1f, BlockSize);

                DrawTexturePro(blocksTex, src, dest, origin, 0.0f, Color.RayWhite);
            }
        }

        morio.Render();

        // Draw gridlines for debugging purposes, do not remove
        if (DrawDebugLines)
        {
            for (int i = 0; i < GridSizeY; i++)
            {
                int LineY = i * (int)BlockSize;
                // Console.WriteLine(LineY);
                DrawLine(0, LineY, WindowWidth, LineY, Color.Black);

            }
            for (int i = 0; i < GridSizeX + 2; i++)
            {
                int LineX = i * (int)BlockSize - (int)morio.x % (int)BlockSize;
                DrawLine(LineX, 0, LineX, WindowHeight, Color.Black);
            }
        }

    }
}
