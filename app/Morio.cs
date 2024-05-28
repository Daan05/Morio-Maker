using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;

class Morio
{
    Texture2D tex;
    public float x;
    public float y;

    const int Speed = 5;

    public Morio()
    {
        tex = LoadTexture("assets/morio.png");
        x = 896;
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
        Vector2 pos = new(856, y);
        DrawTextureEx(tex, pos, 0.0F, 0.4F, Color.RayWhite);
    }
}