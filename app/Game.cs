using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;

class Game
{
    int ScreenWidth;
    int ScreenHeight;

    const float BlockSize = 80.0f;
    int GridSizeX;
    int GridSizeY;

    int MapWidth;
    int MapHeight;

    Tile[,] tiles;
    Morio morio;
    Texture2D blocksTex;
    Rectangle[] blockTextureSourceRects = {
        new(48.0f, 0.0f, 16.0f, 16.0f)
    };

    public Game(int _screenWidth, int _screenHeight)
    {
        ScreenWidth = _screenWidth;
        ScreenHeight = _screenHeight;

        GridSizeX = (int)((float)ScreenWidth / BlockSize);
        GridSizeY = (int)((float)ScreenHeight / BlockSize);

        MapWidth = (int)(GridSizeX * BlockSize);
        MapHeight = (int)(GridSizeY * BlockSize);

        Tile[,] _tiles = new Tile[GridSizeY, GridSizeX];
        for (int j = 0; j < GridSizeY; j++)
        {
            for (int i = 0; i < GridSizeX; i++)
            {
                if (GridSizeY - j <= 2)
                {
                    _tiles[j, i] = new Tile(TileType.Block);
                }
                else
                {
                    _tiles[j, i] = new Tile(TileType.Empty);
                }
            }
        }

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
                if (tiles[j, i].type.GetHashCode() == -1)
                {
                    continue;
                }

                Vector2 pos = new(i * BlockSize, (j + 0.5f) * BlockSize);
                Rectangle src = blockTextureSourceRects[tiles[j, i].type.GetHashCode()];
                Rectangle dest = new(pos, BlockSize, BlockSize);

                DrawTexturePro(blocksTex, src, dest, origin, 0.0f, Color.RayWhite);
            }
        }
        morio.Render();
    }
}