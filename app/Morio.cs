using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;
using static Constants_name.Constants;

class Morio
{
    Texture2D tex;
    public float x;
    public float y;
    Vector2 vel = new(0f, 0f);
    uint frameCount = 0;
    readonly Rectangle[] animationFrames = {
        new(0, 2, 15, 28),
        new(16, 2, 15, 28),
        new(32, 2, 15, 28),
    };

    bool flipped = false; // false is facing to the right

    bool is_grounded = false;
    bool sprinting = false; // false means he's walking, doesn't matter if he's in the air or not

    float targetAnimationFrameTime = 0.080f; // Time a single animationFrame stays on screen
    float sumAnimationFrameTime = 0; // Every animationFrame this gets set to 0 again

    int animationFrameIndex = 0;

    // horizontal movement
    const float Acc = 80;
    const float MaxWalkSpeed = 80;
    const float MaxSprintSpeed = 140;

    const float Resistance = 0.975f;
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

        bool shiftHeld = IsKeyDown(KeyboardKey.LeftShift) || IsKeyDown(KeyboardKey.RightShift);
        bool horKeyPressed = false;

        if (IsKeyDown(KeyboardKey.W))
        {
            vel.Y += Acc * GetFrameTime();
        }
        if (IsKeyDown(KeyboardKey.S))
        {
            vel.Y -= Acc * GetFrameTime();
        }

        if (IsKeyDown(KeyboardKey.Right) || IsKeyDown(KeyboardKey.D))
        {
            HandleHorMovement(true, shiftHeld);
            horKeyPressed = true;
        }
        else if (IsKeyDown(KeyboardKey.Left) || IsKeyDown(KeyboardKey.A))
        {
            HandleHorMovement(false, shiftHeld);
            horKeyPressed = true;
        }

        float maxSpeed = MaxWalkSpeed;
        if (sprinting)
        {
            maxSpeed = MaxSprintSpeed;
        }

        if (vel.X > maxSpeed)
        {
            vel.X = maxSpeed;
        }
        else if (vel.X < -maxSpeed)
        {
            vel.X = -maxSpeed;
        }

        if (!horKeyPressed || !shiftHeld && sprinting)
        {
            if (Math.Abs(vel.X) < MinSpeed && !horKeyPressed)
            {
                // speed below the desired minimal speed, so make speed 0
                vel.X = 0;
                frameCount = 0;
            }
            else
            {
                vel.X *= Resistance;
            }
        }

        // Console.WriteLine(y);
        x += vel.X * GetFrameTime() * BlockSize * 0.1f; // multiply by block size because speed should be based on blocks, not space on the screen
        y += vel.Y * GetFrameTime() * BlockSize * 0.1f; // multiply by 0.1 because it makes the constants easier to work with

        if (x < 0f)
        {
            x = 0;
        }
    }

    public void Render()
    {
        // Console.WriteLine(x);
        Vector2 origin = new(0.0f, 0.0f);
        Rectangle src;

        float frameTime = GetFrameTime();
        sumAnimationFrameTime += frameTime;

        float speed = MathF.Abs(vel.X);
        targetAnimationFrameTime = (100 - speed * 0.5f) / 800;

        if (sumAnimationFrameTime > targetAnimationFrameTime)
        {
            animationFrameIndex++;
            sumAnimationFrameTime = 0f;
        }
        src = animationFrames[animationFrameIndex % 3];

        if (Math.Abs(vel.X) < 0.00001 && Math.Abs(vel.Y) < 0.00001)
        {
            src = animationFrames[0];
            animationFrameIndex = 0;
        }

        Vector2 pos = new(WindowWidth * 0.5f, WindowHeight - y);
        Rectangle dest = new(pos, BlockSize, BlockSize * 2);
        if (flipped)
        {
            src.Width = -src.Width;
        }

        DrawTexturePro(tex, src, dest, origin, 0.0f, Color.RayWhite);
    }

    public void RenderDebugInfo()
    {
        string posText = string.Format("pos: {0} {1}", (int)x, (int)y);
        DrawText(posText, 10, 35, 20, Color.Red);
        string velText = string.Format("vel:  {0} {1}", (int)vel.X, (int)vel.Y);
        DrawText(velText, 10, 55, 20, Color.Red);

        // 4 points that outline Morio's hitbox
        DrawCircle((int)(WindowWidth * 0.5f), (int)(WindowHeight - y), 4, Color.Red);
        DrawCircle((int)(WindowWidth * 0.5f + BlockSize), (int)(WindowHeight - y), 4, Color.Red);
        DrawCircle((int)(WindowWidth * 0.5f), (int)(WindowHeight - y + BlockSize * 2), 4, Color.Red);
        DrawCircle((int)(WindowWidth * 0.5f + BlockSize), (int)(WindowHeight - y + BlockSize * 2), 4, Color.Red);

        // Marks the 2/4 blocks morio is in
        int idX = (int)(x / BlockSize);
        int idY = (int)(y / BlockSize);
        //System.Console.WriteLine(idY + " " + BlockSize);
        Color Grey = new(128, 128, 128, 128);
        DrawRectangle((int)((idX + 15) * BlockSize - x), (int)(WindowHeight - idY * BlockSize + BlockSize), (int)BlockSize, (int)BlockSize, Grey);
        DrawRectangle((int)((idX + 15) * BlockSize - x), (int)(WindowHeight - idY * BlockSize), (int)BlockSize, (int)BlockSize, Grey);
        DrawRectangle((int)((idX + 15) * BlockSize - x), (int)(WindowHeight - idY * BlockSize - BlockSize), (int)BlockSize, (int)BlockSize, Grey);

        DrawRectangle((int)((idX + 16) * BlockSize - x), (int)(WindowHeight - idY * BlockSize + BlockSize), (int)BlockSize, (int)BlockSize, Grey);
        DrawRectangle((int)((idX + 16) * BlockSize - x), (int)(WindowHeight - idY * BlockSize), (int)BlockSize, (int)BlockSize, Grey);
        DrawRectangle((int)((idX + 16) * BlockSize - x), (int)(WindowHeight - idY * BlockSize - BlockSize), (int)BlockSize, (int)BlockSize, Grey);
    }

    void HandleHorMovement(bool to_the_right, bool shiftHeld)
    {
        float acc = Acc;
        if (!to_the_right)
        {
            acc = -acc;
        }
        vel.X += acc * GetFrameTime();

        if (flipped == to_the_right) // mario is turning, so decrease his speed so the turn is smoother
        {
            if (to_the_right && vel.X < -MinSpeed)
            {
                vel.X = -MinSpeed;
            }
            else if (!to_the_right && vel.X > MinSpeed)
            {
                vel.X = MinSpeed;
            }
        }

        if (shiftHeld && Math.Abs(vel.X) > MaxWalkSpeed)
        {
            sprinting = true;
        }
        if (Math.Abs(vel.X) < MaxWalkSpeed)
        {
            sprinting = false;
        }

        flipped = !to_the_right;
    }

    public void SetGrounded()
    {
        vel.Y = 0f;
        is_grounded = true;
    }
}
