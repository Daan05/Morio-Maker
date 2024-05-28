using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;

namespace MorioMaker;

public class Program
{
    const int gridSizeX = 26;
    const int gridSizeY = 18;

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
