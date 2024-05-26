using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;

namespace Examples.Core;

public class InputKeys
{
    public static int Main()
    {
        
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 1200;
        const int screenHeight = 800;
        const int gridSizeX = 25;
        const int gridSizeY = 17;

        InitWindow(screenWidth, screenHeight, "Morio Maker");
        Image windowIcon = LoadImage("assets/morio.png"); 
        SetWindowIcon(windowIcon);

        Vector2 ballPosition = new((float)screenWidth / 2, (float)screenHeight / 2);

        SetTargetFPS(60);       // Set target frames-per-second

        Rectangle src = new(48.0f, 0.0f, 16.0f, 16.0f);
        Texture2D block = LoadTexture("assets/blocks.png");

        Tile[,] tiles = new Tile[gridSizeY, gridSizeX];
        for (int j = 0; j < gridSizeY; j++) {
               for (int i = 0; i < gridSizeX; i++) {
                tiles[j,i] = new Tile(TileType.Block);
            } 
        }

        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())
        {

            // Update
            //----------------------------------------------------------------------------------
            if (IsKeyDown(KeyboardKey.Right))
            {
                ballPosition.X += 2.0f;
            }

            if (IsKeyDown(KeyboardKey.Left))
            {
                ballPosition.X -= 2.0f; //fdsfas
            }

            if (IsKeyDown(KeyboardKey.Up))
            {
                ballPosition.Y -= 2.0f;
            }

            if (IsKeyDown(KeyboardKey.Down))
            {
                ballPosition.Y += 2.0f;
            }
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();
            ClearBackground(Color.RayWhite);

            for (int j = 0; j < gridSizeY; j++) {
               for (int i = 0; i < gridSizeX; i++) {
                    Vector2 pos = new Vector2(i * 48, j * 48);
                    DrawTexturePro(block, src, new Rectangle(pos, new Vector2(48.0f, 48.0f)), new Vector2(0.0f, 0.0f), 0.0f, Color.RayWhite );

                }
            } 
        

            EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        CloseWindow();
        //--------------------------------------------------------------------------------------

        return 0;
    }
}

class Tile {
    TileType type = TileType.Empty;

    public Tile(TileType _type) {
        type = _type;

    }
}

enum TileType {
    Empty,
    Block,
}
