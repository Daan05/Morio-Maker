using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;

namespace MorioMaker;

public class Program
{
    public static int Main()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 1200;
        const int screenHeight = 800;

        const int gridSizeX = 60;
        const int gridSizeY = 20;
        const float blockSize = 48.0f;

        const int mapWidth = (int)(gridSizeX * blockSize);
        const int mapHeight = (int)(gridSizeY * blockSize);

        InitWindow(screenWidth, screenHeight, "Morio Maker");
        Image windowIcon = LoadImage("assets/morio.png"); 
        SetWindowIcon(windowIcon);

        SetTargetFPS(60);       // Set target frames-per-second

        Texture2D blocks = LoadTexture("assets/blocks.png");
        Rectangle[] blockTextureSourceRects = { 
            new Rectangle(48.0f, 0.0f, 16.0f, 16.0f)
        };

        Tile[,] tiles = new Tile[gridSizeY, gridSizeX];
        for (int j = 0; j < gridSizeY; j++) {
               for (int i = 0; i < gridSizeX; i++) {
                if (j > 14) {
                    tiles[j,i] = new Tile(TileType.Block);
                } else {
                    tiles[j,i] = new Tile(TileType.Empty);
                }
            } 
        }

        Vector2 mapPos = new Vector2(720, 400);

        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())
        {
            // Update
            //----------------------------------------------------------------------------------
            if (IsKeyDown(KeyboardKey.Right))
            {
                // Stay in bounds of map
                if (mapPos.X > 13.0f * blockSize) {
                    mapPos.X -= 2.0f;
                }
            }

            if (IsKeyDown(KeyboardKey.Left))
            {
                // Stay in bounds of map
                if (mapPos.X < mapWidth - 13.0f * blockSize) {
                    mapPos.X += 2.0f;
                }
            }

            if (IsKeyDown(KeyboardKey.Up))
            {
                mapPos.Y -= 2.0f;
            }

            if (IsKeyDown(KeyboardKey.Down))
            {
                mapPos.Y += 2.0f;
            }
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();
            ClearBackground(Color.RayWhite);

            for (int j = 0; j < gridSizeY; j++) {
               for (int i = 0; i < gridSizeX; i++) {

                    // Loop through all tiles
                    // Skip if the tile is empty
                    if (tiles[j, i].type.GetHashCode() == -1) {
                        continue;
                    }
                    
                    Vector2 pos = new Vector2(i * blockSize, j * blockSize);
                    //              tex         src                                                         dst                                                 origin              rot        tint
                    DrawTexturePro(blocks, blockTextureSourceRects[tiles[j, i].type.GetHashCode()], new Rectangle(pos, new Vector2(blockSize, blockSize)), new Vector2(0.0f, 0.0f), 0.0f, Color.RayWhite );
                }
            } 
        
            DrawFPS(10, 10);
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
    public TileType type = TileType.Empty;

    public Tile(TileType _type) {
        type = _type;

    }
}

enum TileType {
    Empty = -1,
    Block = 0,
}
