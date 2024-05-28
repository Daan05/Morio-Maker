using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;

namespace MorioMaker;

public class Program
{
    // these consts are defined twice, should be fixed
    public const int ScreenWidth = 1200;
    public const int ScreenHeight = 816;

    public static int Main()
    {
        // Initialization
        //--------------------------------------------------------------------------------------


        InitWindow(ScreenWidth, ScreenHeight, "Morio Maker");
        Image windowIcon = LoadImage("assets/morio.png");
        SetWindowIcon(windowIcon);
        SetTargetFPS(60);       // Set target frames-per-second

        //--------------------------------------------------------------------------------------

        Game game = new Game();

        // Main loop
        while (!WindowShouldClose())
        {
            DrawFPS(10, 10);

            game.Update();

            // Render
            BeginDrawing();
            ClearBackground(Color.RayWhite);

            game.Render();

            EndDrawing();
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        CloseWindow();
        //--------------------------------------------------------------------------------------

        return 0;
    }

}

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


class Game
{
    // these consts are defined twice, should be fixed
    public const int ScreenWidth = 1200; // these consts get exact integers when you divide by 48
    public const int ScreenHeight = 816;

    const float BlockSize = 48.0f;
    const int GridSizeX = ScreenWidth / (int)BlockSize;
    const int GridSizeY = ScreenHeight / (int)BlockSize;

    const int MapWidth = (int)(GridSizeX * BlockSize);
    const int MapHeight = (int)(GridSizeY * BlockSize);

    Tile[,] tiles;
    Morio morio;
    Texture2D blocksTex;
    Rectangle[] blockTextureSourceRects = {
            new(48.0f, 0.0f, 16.0f, 16.0f)
        };

    public Game()
    {
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
        Vector2 origin = new Vector2(BlockSize, BlockSize);
        for (int j = 0; j < GridSizeY; j++)
        {
            for (int i = 0; i < GridSizeX; i++)
            {
                if (tiles[j, i].type.GetHashCode() == -1)
                {
                    continue;
                }

                Vector2 pos = new(i * BlockSize, j * BlockSize);
                Rectangle src = blockTextureSourceRects[tiles[j, i].type.GetHashCode()];
                Rectangle dest = new(pos, origin);

                DrawTexturePro(blocksTex, src, dest, origin, 0.0f, Color.RayWhite);
            }
        }
        morio.Render();
    }
}

class Morio
{
    Texture2D tex;
    int x; // these probably need to be converted to floats
    int y;

    const int Speed = 5;

    public Morio()
    {
        tex = LoadTexture("assets/morio.png");
        x = 0;
        y = 0;
    }

    public void Update()
    {

        if (IsKeyDown(KeyboardKey.Right))
        {
            x += Speed;
        }

        if (IsKeyDown(KeyboardKey.Left))
        {
            x -= Speed;
        }

        if (IsKeyDown(KeyboardKey.Up))
        {
            y -= Speed;
        }

        if (IsKeyDown(KeyboardKey.Down))
        {
            y += Speed;
        }
    }

    public void Render()
    {
        Vector2 pos = new Vector2(x, y);
        DrawTextureEx(tex, pos, 0.0F, 0.4F, Color.RayWhite);
    }
}