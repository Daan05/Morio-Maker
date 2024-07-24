using System.Numerics;
using Raylib_cs;

using static Raylib_cs.Raylib;
using static Constants_name.Constants;

class Morio
{
    Texture2D tex;
    public float x;
    public float y;
    public Vector2 vel = new(0f, 0f);
    readonly Rectangle[] animationFrames = {
        new(0, 2, 15, 28),
        new(16, 2, 15, 28),
        new(32, 2, 15, 28),
    };

    bool flipped = false; // false = facing to the right

    bool is_grounded = false; // true = able to jump
    bool sprinting = false; // false means he's walking, doesn't matter if he's in the air or not

    float targetAnimationFrameTime = 0.080f; // Time a single animationFrame stays on screen
    public float sumAnimationFrameTime = 0; // Every animationFrame this gets set to 0 again

    int animationFrameIndex = 0;

    // horizontal movement
    const float Acc = 80;
    const float MaxWalkSpeed = 80;
    const float MaxSprintSpeed = 140;

    const float Resistance = 0.975f;
    const float MinSpeed = 20;

    // vertical movement
    const float JumpVel = 150;
    const float GravMultIfJumpHeld = 0.5f;
    const float Gravity = -500;


    public Morio()
    {
        tex = LoadTexture("assets/morios.png");
        x = WindowWidth / 2;
        y = WindowHeight / 2;
    }

    public void Update()
    {
        float gravMult = 1;

        // Handle jump event
        // if space bar is helt for longer Morio will jump higher
        if (IsKeyDown(KeyboardKey.Space))
        {
            if (is_grounded)
            {
                vel.Y = JumpVel;
                is_grounded = false;
            }
            else if (vel.Y >= 0)
            {
                gravMult = GravMultIfJumpHeld;
            }
        }

        // Jump if Morio is on the ground
        if (!is_grounded)
        {
            vel.Y += Gravity * gravMult * GetFrameTime();
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

        // Move horizontaly
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

        // Handle walking/sprinting
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

        // Slow morio down when going to fast
        // Either when not holding a horizontal move key or releasing shift while sprinting
        if (!horKeyPressed || !shiftHeld && sprinting)
        {
            if (Math.Abs(vel.X) < MinSpeed && !horKeyPressed)
            {
                // speed below the desired minimal speed, so make speed 0
                vel.X = 0;
            }
            else
            {
                vel.X *= Resistance;
            }
        }

        // Move Morio
        x += vel.X * GetFrameTime() * BlockSize * 0.1f; // multiply by block size because speed should be based on blocks, not space on the screen
        y += vel.Y * GetFrameTime() * BlockSize * 0.1f; // multiply by 0.1 because it makes the constants easier to work with

        // Keep Morio on the platform part
        // Move to Game.cs where Morio collisions get checked????
        // if (x < 0f)
        // {
        //     x = 0;
        // }
        // else if (x > value)

        // Set is_grounded to false
        // this is so gravity gets applied, otherwise Morio won't fall when walking off a platform
        // if Morio is actually grounded this will get rectified in the collision testing code
        is_grounded = false;
    }

    public void Render()
    {
        Vector2 origin = new(0.0f, 0.0f);
        Rectangle src;


        sumAnimationFrameTime += GetFrameTime();

        // The faster Morio moves the faster his animation should be
        float speed = MathF.Abs(vel.X);
        targetAnimationFrameTime = (100 - speed * 0.5f) / 800;

        // If time to go to next animation frame
        if (sumAnimationFrameTime > targetAnimationFrameTime)
        {
            animationFrameIndex++;
            sumAnimationFrameTime = 0f;
        }
        src = animationFrames[animationFrameIndex % 3];

        // Select the right animation frame for Morio standing still
        if (Math.Abs(vel.X) < 0.00001 && Math.Abs(vel.Y) < 0.00001)
        {
            src = animationFrames[0];
            animationFrameIndex = 0;
        }

        // Draw parameters
        Vector2 pos = new(WindowWidth * 0.5f, WindowHeight - y);
        Rectangle dest = new(pos, BlockSize, BlockSize * 2);
        if (flipped)
        {
            src.Width = -src.Width; // Setting the width to a negative value flips the image
        }

        //Draw
        DrawTexturePro(tex, src, dest, origin, 0.0f, Color.RayWhite);
    }

    public void RenderDebugInfo()
    {
        // Draw Morio's pos and vel in left top corner
        string posText = string.Format("pos: {0} {1}", (int)x, (int)y);
        DrawText(posText, 10, 35, 20, Color.Red);
        string velText = string.Format("vel:  {0} {1}", (int)vel.X, (int)vel.Y);
        DrawText(velText, 10, 55, 20, Color.Red);

        // 4 points that outline Morio's hitbox
        DrawCircle((int)(WindowWidth * 0.5f), (int)(WindowHeight - y), 4, Color.Red);
        DrawCircle((int)(WindowWidth * 0.5f + BlockSize), (int)(WindowHeight - y), 4, Color.Red);
        DrawCircle((int)(WindowWidth * 0.5f), (int)(WindowHeight - y + BlockSize * 2), 4, Color.Red);
        DrawCircle((int)(WindowWidth * 0.5f + BlockSize), (int)(WindowHeight - y + BlockSize * 2), 4, Color.Red);

        // Marks the 6 blocks morio is in
        int idX = (int)(x / BlockSize);
        int idY = (int)(y / BlockSize);
        Color Grey = new(128, 128, 128, 128); // transparant grey color

        // left 3 blocks
        DrawRectangle((int)((idX + 15) * BlockSize - x), (int)(WindowHeight - idY * BlockSize + BlockSize), (int)BlockSize, (int)BlockSize, Grey);
        DrawRectangle((int)((idX + 15) * BlockSize - x), (int)(WindowHeight - idY * BlockSize), (int)BlockSize, (int)BlockSize, Grey);
        DrawRectangle((int)((idX + 15) * BlockSize - x), (int)(WindowHeight - idY * BlockSize - BlockSize), (int)BlockSize, (int)BlockSize, Grey);
        // right 3 blocks
        DrawRectangle((int)((idX + 16) * BlockSize - x), (int)(WindowHeight - idY * BlockSize + BlockSize), (int)BlockSize, (int)BlockSize, Grey);
        DrawRectangle((int)((idX + 16) * BlockSize - x), (int)(WindowHeight - idY * BlockSize), (int)BlockSize, (int)BlockSize, Grey);
        DrawRectangle((int)((idX + 16) * BlockSize - x), (int)(WindowHeight - idY * BlockSize - BlockSize), (int)BlockSize, (int)BlockSize, Grey);
    }

    void HandleHorMovement(bool to_the_right, bool shiftHeld)
    {
        // Set the acceleration for going the correct direction
        float acc = Acc;
        if (!to_the_right)
        {
            acc = -acc;
        }
        vel.X += acc * GetFrameTime();

        // If Morio is turning, decrease his speed so the turn is smoother
        if (flipped == to_the_right)
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

        // Is Morio sprinting
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
        // Morio is standing on the ground
        vel.Y = 0f;
        is_grounded = true;
    }
}
