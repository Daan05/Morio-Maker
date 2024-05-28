using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;

namespace MorioMaker;

public class Program
{
    static public void Main()
    {
        // Initialization
        //--------------------------------------------------------------------------------------

        const int screenWidth = 1792;
        const int screenHeight = 1024;

        InitWindow(screenWidth, screenHeight, "Morio Maker");
        Image windowIcon = LoadImage("assets/morio.png"); 
        SetWindowIcon(windowIcon);

        SetTargetFPS(60);

        Game game = new Game(screenWidth, screenHeight);

        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())
        {     
            // Update
            game.Update();

            // Render
            BeginDrawing();
            ClearBackground(Color.RayWhite);

            game.Render();

            DrawFPS(10, 10);
            EndDrawing();
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------

        CloseWindow();

        //--------------------------------------------------------------------------------------
    }
}
