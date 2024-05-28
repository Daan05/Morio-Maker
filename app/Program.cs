using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;

namespace MorioMaker;

public class Program
{
    // Initialization
    //--------------------------------------------------------------------------------------
    
    const int screenWidth = 1200;
    const int screenHeight = 816;

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
