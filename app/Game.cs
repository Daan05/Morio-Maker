using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Constants_name.Constants;

class Game
{
    readonly int GridSizeX;
    readonly int GridSizeY;

    bool debugModeEnabled = false;

    readonly TileType[,] tiles;
    readonly Morio morio = new();

    Texture2D blocksTex = LoadTexture("assets/tiles.png");
    Texture2D backgroundTex = LoadTexture("assets/back.png");


    public Game()
    {
        GridSizeX = (int)(WindowWidth / BlockSize);
        GridSizeY = (int)(WindowHeight / BlockSize);

        if (WindowHeight % BlockSize == 0 && WindowWidth % BlockSize == 0)
        {
            Console.WriteLine("screensize matches blocksize");
            Console.WriteLine(GridSizeY);
        }

        TileType[,] _tiles = new TileType[GridSizeY, GridSizeX * 8];
                    Console.WriteLine(_tiles.GetLength(0));


        for (int j = 0; j < _tiles.GetLength(0); j++) // y
        {
            for (int i = 0; i < _tiles.GetLength(1); i++) // x
            {
                _tiles[j, i] = TileType.Empty;
                if (j == GridSizeY - 1)
                {
                    _tiles[j, i] = TileType.Grass_MM;
                }
                else if (j == GridSizeY - 2)
                {
                    _tiles[j, i] = TileType.Grass_TM;
                }
            }
        }

        _tiles[11, 25] = TileType.Platform_L;
        _tiles[11, 26] = TileType.Platform_M;
        _tiles[11, 27] = TileType.Platform_R;


        // _tiles[12, 25] = TileType.Platform_L;
        // _tiles[12, 26] = TileType.Platform_M;
        // _tiles[12, 27] = TileType.Platform_R;


        _tiles[13, 20] = TileType.Platform_R;

        tiles = _tiles;
    }

    public void Update()
    {
        if (IsKeyPressed(KeyboardKey.E))
            debugModeEnabled = !debugModeEnabled;

        morio.Update();

        // Check Morio collisions with tiles
        // -------------------------------------------------
        int idX = (int)(morio.x / BlockSize); // left
        int idY = (int)(morio.y / BlockSize); // top

        int indexY = GridSizeY - idY;

        float x = 0;
        float y = 0;

        // check horizontal collisions with tiles
        if (morio.vel.X > 0 && (tiles[indexY, idX + 1].GetHashCode() != -1 || tiles[indexY + 1, idX + 1].GetHashCode() != -1)) // if inside of solid block
        {
            x = idX * BlockSize - morio.x; 
            
            // move morio left
            // morio.x = idX * BlockSize;
            // morio.vel.X = 0;
            morio.sumAnimationFrameTime = 0;
        }
        else if (morio.vel.X < 0 && (tiles[indexY, idX].GetHashCode() != -1 || tiles[indexY + 1, idX].GetHashCode() != -1)) // if inside of solid block
        {
            x = (idX + 1) * BlockSize - morio.x;
            // move morio right
            // morio.x = (idX + 1) * BlockSize;
            // morio.vel.X = 0;
            morio.sumAnimationFrameTime = 0;
        }

        // Check vertical collisions with tiles
        if (morio.vel.Y > 0 && (tiles[indexY - 1, idX].GetHashCode() != -1 || tiles[indexY - 1, idX + 1].GetHashCode() != -1)) // if inside of solid block
        {
            y = morio.y - (GridSizeY - indexY - 0) * BlockSize;
            
            // Console.WriteLine("top collision");
            // Move Morio down
            // morio.y = (GridSizeY - indexY) * BlockSize;
            // morio.vel.Y = 0;
        }
        else if (morio.vel.Y < 0 && (tiles[indexY + 1, idX].GetHashCode() != -1 || tiles[indexY + 1, idX + 1].GetHashCode() != -1)) // if inside of solid block
        {
            y = (GridSizeY - indexY + 1) * BlockSize - morio.y;
            // Console.WriteLine("bottom collision");
            // Move Morio up
            // morio.y = (GridSizeY - indexY + 1) * BlockSize;
            // morio.SetGrounded();
        }

        if (x != 0 && Math.Abs(x) < Math.Abs(y)) {
            morio.x = (int)((morio.x + x) / BlockSize) * BlockSize;
            // morio.x += x;
            morio.vel.X = 0;
            Console.WriteLine("x is " + morio.x);
        } else if (y != 0) {
            morio.y = (int)((morio.y + y) / BlockSize) * BlockSize;
            morio.SetGrounded();
            // Console.WriteLine("y is " + y);
        }
    }

    public void Render()
    {
        // Render background
        if (!debugModeEnabled)
        {
            Rectangle src = new(0, 0, 512f, backgroundTex.Height);
            float backTexSpeed = 0.3f;
            float backgroundPos = backTexSpeed * -morio.x % WindowWidth;
            Rectangle dest = new(backgroundPos, 0, WindowWidth, WindowHeight);

            DrawTexturePro(backgroundTex, src, dest, new(0, 0), 0, Color.RayWhite);
            dest.X += WindowWidth;
            DrawTexturePro(backgroundTex, src, dest, new(0, 0), 0, Color.RayWhite);
        }

        // Render tiles
        for (int j = 0; j < tiles.GetLength(0); j++) // y
        {
            for (int i = 0; i < tiles.GetLength(1); i++) // x
            {
                if (tiles[j, i].GetHashCode() == -1)
                {
                    continue;
                }

                Vector2 pos = new(i * BlockSize - morio.x + WindowWidth * 0.5f, j * BlockSize);
                Rectangle src = blockTexSourceRects[tiles[j, i].GetHashCode()];
                Rectangle dest = new(pos, BlockSize + 1, BlockSize);

                DrawTexturePro(blocksTex, src, dest, new(0, 0), 0, Color.RayWhite);
            }
        }

        // Render Morio
        morio.Render();

        // Draw gridlines for debugging purposes, do not remove
        if (debugModeEnabled)
        {
            DrawFPS(10, 10);

            // Horizontal lines
            for (int i = 0; i < GridSizeY; i++)
            {
                int LineY = i * (int)BlockSize;
                DrawLine(0, LineY, WindowWidth, LineY, Color.Black);

            }
            // Vertical lines
            for (int i = 0; i < GridSizeX + 2; i++)
            {
                int LineX = i * (int)BlockSize - (int)morio.x % (int)BlockSize;
                DrawLine(LineX, 0, LineX, WindowHeight, Color.Black);
            }

            morio.RenderDebugInfo();
        }
    }
}
