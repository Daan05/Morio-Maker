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
    public static int Main()
    {
        
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 800;
        const int gridSizeX = 8;
        const int gridSizeY = 8;

        InitWindow(screenWidth, screenHeight, "raylib [core] example - keyboard input");

        Vector2 ballPosition = new((float)screenWidth / 2, (float)screenHeight / 2);

        SetTargetFPS(60);       // Set target frames-per-second

        Rectangle src = new(0, 0, 100, 100);
        Texture2D mario = LoadTexture("mario.png");

        Tile[,] tiles = new Tile[gridSizeY, gridSizeX];
        for (int j = 0; j < gridSizeY; j++) {
               for (int i = 0; i < gridSizeX; i++) {
                tiles[j,i] = new Tile(TileType.Mario);
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
                    Vector2 pos = new Vector2(i * 100, j * 100);
                    DrawTextureEx(mario, pos, 0.0F, 0.4F, Color.RayWhite );
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
    TileType type = TileType.Mario;

    public Tile(TileType _type) {
        type = _type;

    }
}

enum TileType {
    Empty,
    Mario,
    Block,
}


/*
class Game 
    tiles: List<List<Tile>>

class Tile 
    type: TileType

enum TileType
    Empty
    Mario
    Block
    etc.



*/