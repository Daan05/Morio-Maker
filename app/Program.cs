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
        // FindSuitableBlockSize();

        SetTraceLogLevel(TraceLogLevel.Error);
        InitWindow(WindowWidth, WindowHeight, "Morio Maker");

        Image windowIcon = LoadImage("assets/morio.png");
        SetWindowIcon(windowIcon);

        SetTargetFPS(144);
        // SetWindowState(ConfigFlags.VSyncHint);

        Game game = new();

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
        CloseWindow();
    }

    // Useful for finding block size that match the window size
    // static void FindSuitableBlockSize()
    // {
    //     for (int i = 1; i < WindowWidth / 8; i++)
    //     {
    //         if ((float)WindowWidth / (float)i % 1 == 0 && (float)WindowHeight / (float)i % 1 == 0)
    //         {
    //             Console.WriteLine(i + " gives " + (float)WindowWidth / (float)i);

    //         }
    //     }
    // }
}

