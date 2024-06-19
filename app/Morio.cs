using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;
using static Constants;

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
    };

    bool flipped = false; // false is facing to the right

    bool is_grounded = false;

    // these numbers definitely need to be tweaked
    const float Acc = 80;
    const float MaxSpeed = 80;
    const float Resistance = 0.98f;

    // jumping
    const float JumpVel = 180; 
    const float GravMultIfJumpHeld = 0.5f;
    const float Gravity = -700;


    public Morio()
    {
        tex = LoadTexture("assets/morios.png");
        x = ScreenWidth / 2;
        y = ScreenHeight / 2;
    }

    public void Update()
    {
        bool j = false;
        frameCount++;

        if (y <= BlockSize * 3f)
        {
            y = BlockSize * 3f;
            vel.Y = 0f;
            is_grounded = true;
        }

        if (IsKeyDown(KeyboardKey.Space))
        {
            if (is_grounded) {
                vel.Y = JumpVel;
                            is_grounded = false;
            } else {
                j = true;
            }            
        }

        if (!is_grounded)
        {
            if (j && vel.Y >= 0f) {
                vel.Y += Gravity * GravMultIfJumpHeld * GetFrameTime();

            } else { vel.Y += Gravity * GetFrameTime();}
        }

        if (IsKeyDown(KeyboardKey.Right) || IsKeyDown(KeyboardKey.D))
        {
            vel.X += Acc * GetFrameTime();
            if (flipped)
            {
                if (vel.X < -20.0f)
                {
                    vel.X = -20.0f;
                }
                else
                {
                    vel.X = 0.0f;
                    frameCount = 0;
                }
            }

            flipped = false;
        }
        else if (IsKeyDown(KeyboardKey.Left) || IsKeyDown(KeyboardKey.A))
        {
            vel.X -= Acc * GetFrameTime();
            if (!flipped)
            {
                if (vel.X > 20.0f)
                {
                    vel.X = 20.0f;
                }
                else
                {
                    vel.X = 0.0f;
                    frameCount = 0;
                }
            }

            flipped = true;
        }
        else
        {
            if (vel.X > 20.0f)
            {
                vel.X *= Resistance;
            }
            else if (vel.X < -20.0f)
            {
                vel.X *= Resistance;
            }
            else
            {
                vel.X = 0.0f;
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

        // System.Console.WriteLine(y);
        x += vel.X * GetFrameTime() * BlockSize * 0.1f; // multiply by block size because speed should be based on blocks, not space on the screen
        y += vel.Y * GetFrameTime() * BlockSize * 0.1f; // multiply by 0.1 because it makes the constants easier to work with
    }

    public void Render()
    {
        // System.Console.WriteLine(x);
        Vector2 origin = new(0.0f, 0.0f);
        Rectangle src;

        int newFrameTime = (int)((float)frameCount * GetFrameTime() * 12f);
        src = animationFrames[newFrameTime % 3];
        // System.Console.WriteLine(newFrameTime);

        Vector2 pos = new(ScreenWidth * 0.5f, ScreenHeight - y);
        Rectangle dest = new(pos, BlockSize, BlockSize * 2);
        if (flipped)
        {
            src.Width = -src.Width;
        }

        DrawTexturePro(tex, src, dest, origin, 0.0f, Color.RayWhite);
    }
}