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

    // horizontal movement
    const float Acc = 80;
    const float MaxSpeed = 80;
    const float Resistance = 0.97f;
    const float MinSpeed = 20;

    // vertical movement
    const float JumpVel = 180;
    const float GravMultIfJumpHeld = 0.5f;
    const float Gravity = -700;


    public Morio()
    {
        tex = LoadTexture("assets/morios.png");
        x = WindowWidth / 2;
        y = WindowHeight / 2;
    }

    public void Update()
    {
        frameCount++;
        float grav_mult = 1;

        if (y <= BlockSize * 4f)
        {
            y = BlockSize * 4f;
            vel.Y = 0f;
            is_grounded = true;
        }

        if (IsKeyDown(KeyboardKey.Space))
        {
            if (is_grounded)
            {
                vel.Y = JumpVel;
                is_grounded = false;
            }
            else if (vel.Y >= 0)
            {
                grav_mult = GravMultIfJumpHeld;
            }
        }

        if (!is_grounded)
        {
            vel.Y += Gravity * grav_mult * GetFrameTime();
        }

        if (IsKeyDown(KeyboardKey.Right) || IsKeyDown(KeyboardKey.D))
        {
            HandleHorMovement(true);
        }
        else if (IsKeyDown(KeyboardKey.Left) || IsKeyDown(KeyboardKey.A))
        {
            HandleHorMovement(false);
        }
        else
        {
            if (vel.X < -MinSpeed || vel.X > MinSpeed)
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

        // Console.WriteLine(y);
        x += vel.X * GetFrameTime() * BlockSize * 0.1f; // multiply by block size because speed should be based on blocks, not space on the screen
        y += vel.Y * GetFrameTime() * BlockSize * 0.1f; // multiply by 0.1 because it makes the constants easier to work with
    }

    public void Render()
    {
        // Console.WriteLine(x);
        Vector2 origin = new(0.0f, 0.0f);
        Rectangle src;

        int newFrameTime = (int)((float)frameCount * GetFrameTime() * 12f);
        src = animationFrames[newFrameTime % 3];
        // Console.WriteLine(newFrameTime);

        Vector2 pos = new(WindowWidth * 0.5f, WindowHeight - y);
        Rectangle dest = new(pos, BlockSize, BlockSize * 2);
        if (flipped)
        {
            src.Width = -src.Width;
        }

        DrawTexturePro(tex, src, dest, origin, 0.0f, Color.RayWhite);
    }

    void HandleHorMovement(bool to_the_right)
    {
        float acc = Acc;
        if (!to_the_right)
        {
            acc = -Acc;
        }

        vel.X += acc * GetFrameTime();
        if (flipped && to_the_right || !flipped && !to_the_right)
        {
            if (to_the_right && vel.X < -MinSpeed)
            {
                vel.X = -MinSpeed;
            }
            else if (!to_the_right && vel.X > MinSpeed)
            {
                vel.X = MinSpeed;
            }
            else
            {
                vel.X = 0.0f;
                frameCount = 0;
            }
        }

        flipped = !to_the_right;
    }
}