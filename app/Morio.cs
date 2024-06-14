using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;

class Morio
{
    Texture2D tex;
    public float x;
    public float y;
    Vector2 vel = new(0f, 0f);

    uint frameCount = 0;
    readonly Rectangle[] animationFrames = {
        new(0, 0, 16, 30),
        new(16, 0, 16, 30),
        new(32, 0, 16, 30),
        // new(18, 0, 18, 36),
        // new(36, 0, 18, 36),
        // new(54, 0, 18, 36),
        // new(72, 0, 18, 36),
        // new(90, 0, 18, 36),
        // new(108, 0, 18, 36),
        // new(126, 0, 18, 36),
        // new(144, 0, 18, 36),
        // new(162, 0, 18, 36),
        // new(180, 0, 18, 36),
        // new(198, 0, 18, 36),
        // new(216, 0, 18, 36),
        // new(234, 0, 18, 36),
        // new(252, 0, 18, 36),

    };

    // these numbers definitely need to be tweaked
    const int Acc = 500;
    const int MaxSpeed = 700;
    const int Resistance = 1000;
    bool moving = false;
    bool flipped = false; // false is facing to the right

    public Morio()
    {
        tex = LoadTexture("assets/morios.png");
        x = 896;
        y = 1008 - 112 * 3;
    }

    public void Update()
    {
        frameCount++; 

        if (IsKeyDown(KeyboardKey.Right) || IsKeyDown(KeyboardKey.D))
        {
            vel.X += Acc * GetFrameTime();
            if (flipped && vel.X < -200.0f)
            {
                if (vel.X < 200.0f)
                {
                    vel.X = -200.0f;
                }
                else
                {
                    vel.X = 0.0f;
                }
            }

            moving = true;
            flipped = false;
        }
        else if (IsKeyDown(KeyboardKey.Left) || IsKeyDown(KeyboardKey.A))
        {
            vel.X -= Acc * GetFrameTime();
            if (!flipped)
            {
                if (vel.X > 200.0f)
                {
                    vel.X = 200.0f;
                }
                else
                {
                    vel.X = 0.0f;
                }
            }

            moving = true;
            flipped = true;
        }
        else
        {
            if (vel.X > 50.0f)
            {
                vel.X -= Resistance * GetFrameTime();
            }
            else if (vel.X < -50.0f)
            {
                vel.X += Resistance * GetFrameTime();
            }
            else
            {
                vel.X = 0.0f;
                moving = false;
                frameCount = 0;
            }

        }
        if (vel.X > MaxSpeed)
        {
            vel.X = MaxSpeed;
        }
        else if (vel.X < -MaxSpeed)
        {
            vel.X = -MaxSpeed;
        }

        // System.Console.WriteLine(vel.X);
        x += vel.X * GetFrameTime();
    }

    public void Render()
    {
        Vector2 origin = new(0.0f, 0.0f);
        Rectangle src;
        if (moving)
        {
            src = animationFrames[((frameCount / 5) % 3)];
        }
        else
        {
            src = animationFrames[0];
        }
        Vector2 pos = new(887, y);
        Rectangle dest = new(pos, 112, 224);
        if (flipped)
        {
            src.Width = -src.Width;
        }

        DrawTexturePro(tex, src, dest, origin, 0.0f, Color.RayWhite);
        // ((frameCount / 10) % 3) + 4
        // framecount / 10 = every animation tick on screen for 1/6th second (at 60 fps)
        // % 3 = there are 3 images that are part of the walking animation
        // + 4 = start at index 4 (so index 4, 5, 6 are for walking)
    }
}