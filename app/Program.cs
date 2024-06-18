using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;
using static Constants;

namespace MorioMaker;

public class Program
{
    static public void Main()
    {
        // Initialization
        //--------------------------------------------------------------------------------------

        SetTraceLogLevel(TraceLogLevel.Error);
        InitWindow(ScreenWidth, ScreenHeight, "Morio Maker");

        Image windowIcon = LoadImage("assets/morio.png");
        SetWindowIcon(windowIcon);

        SetTargetFPS(60);

        Game game = new();

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