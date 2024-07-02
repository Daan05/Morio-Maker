using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;
using static Constants_name.Constants;

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

        SetTargetFPS(TargetFps);

        Game game = new();

        // Main game loop
        while (!WindowShouldClose())
        {
            // Move stuff
            game.Update();

            // Render Stuff
            BeginDrawing();
            ClearBackground(Color.RayWhite);

            game.Render();

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

