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

        const int ScreenWidth = 1792;
        const int ScreenHeight = 1008;

        SetTraceLogLevel(TraceLogLevel.Error);
        InitWindow(ScreenWidth, ScreenHeight, "Morio Maker");

        Image windowIcon = LoadImage("assets/morio.png");
        SetWindowIcon(windowIcon);

        SetTargetFPS(60);

        Game game = new(ScreenWidth, ScreenHeight);

        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())
        {
            game.Update();

            // Render
            BeginDrawing();
            ClearBackground(Color.RayWhite);

            DrawFPS(10, 10);
            game.Render();

            DrawFPS(10, 10);
            EndDrawing();
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------

        CloseWindow();
    }
}