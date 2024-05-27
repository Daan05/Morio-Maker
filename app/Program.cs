/*******************************************************************************************
*
*   raylib [core] example - Keyboard input
*
*   This example has been created using raylib 1.0 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2014 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;

namespace Examples.Core;

public class InputKeys
{
    const int gridSizeX = 26;
    const int gridSizeY = 18;

    public static int Main()
    {

        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 1200;
        const int screenHeight = 816;


        InitWindow(screenWidth, screenHeight, "Morio Maker");

        Image windowIcon = LoadImage("assets/morio.png"); 
        SetWindowIcon(windowIcon);

        SetTargetFPS(60);

        Game game = new Game();

        // Main game loop
        while (!WindowShouldClose())
        {
            // Update
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

    class Game
    {
        Tile[,] tiles;
        Morio morio;
        Texture2D block_tex;

        public Game()
        {
            Tile[,] _tiles = new Tile[gridSizeY, gridSizeX];
            for (int j = 0; j < gridSizeY; j++)
            {
                for (int i = 0; i < gridSizeX; i++)
                {
                    if (gridSizeY - j <= 2)
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
            block_tex = LoadTexture("assets/blocks.png");
        }

        public void Update()
        {
            morio.Update();
        }

        public void Render()
        {
            Rectangle src = new(48.0f, 0.0f, 16.0f, 16.0f);
            Vector2 origin = new Vector2(48.0f, 48.0f);

            for (int j = 0; j < gridSizeY; j++)
            {
                for (int i = 0; i < gridSizeX; i++)
                {
                    if (tiles[j, i].type == TileType.Block)
                    {
                        Vector2 pos = new Vector2(i * 48.0f, j * 48.0f);
                        Rectangle dest = new Rectangle(pos, origin);

                        DrawTexturePro(block_tex, src, dest, origin, 0.0f, Color.RayWhite);
                    }

                }
            }

            morio.Render();
        }
    }

    class Tile
    {
        public TileType type = TileType.Block;

        public Tile(TileType _type)
        {
            type = _type;
        }
    }

    enum TileType
    {
        Empty,
        Block,
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


