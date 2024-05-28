using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;

class Morio
{
    Texture2D tex;
    public float x;
    public float y;

    uint frameCount = 0; 
    Rectangle[] animationFrames = {
        new(16, 8, 16, 32),
    };

    const int Speed = 5;
    bool moving = false;

    public Morio()
    {
        tex = LoadTexture("assets/newmorio.png");
        x = 896;
        y = 0;
    }

    public void Update()
    {
        if (IsKeyDown(KeyboardKey.Right))
        {
            x += Speed;
            moving = true;
            frameCount++;
        }
        else if (IsKeyDown(KeyboardKey.Left))
        {
            x -= Speed;
            moving = true;
            frameCount++;
        }
        else
        {
            moving = false;
            frameCount = 0;
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
        Vector2 origin = new(0.0f, 0.0f);
        Rectangle src;
        if (moving)
        {
            src = animationFrames[((frameCount / 10) % 1) + 0];
        }
        else
        {
            src = animationFrames[0];
        }
        Vector2 pos = new(887, y);
        Rectangle dest = new(pos, 108, 216);
        
        DrawTexturePro(tex, src, dest, origin, 0.0f, Color.RayWhite);
        // ((frameCount / 10) % 3) + 4
        // framecount / 10 = every animation tick on screen for 1/6th second (at 60 fps)
        // % 3 = there are 3 images that are part of the walking animation
        // + 4 = start at index 4 (so index 4, 5, 6 are for walking)
    }
}