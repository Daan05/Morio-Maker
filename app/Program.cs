using Raylib_cs;

namespace MorioMaker
{
    public class Program
    {
        public static int Main()
        {
            const int screenWidth = 800;
            const int screenHeight = 450;

            Raylib.InitWindow(screenWidth, screenHeight, "raylib [core] example - basic window");

            Raylib.SetTargetFPS(60);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RayWhite);

                Raylib.DrawText("Congrats! You created your first window!", 190, 200, 20, Color.Maroon);

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();

            return 0;
        }
    }
}