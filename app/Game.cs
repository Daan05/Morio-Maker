using System.Globalization;
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

        if (WindowHeight % BlockSize != 0 || WindowWidth % BlockSize != 0 && GridSizeX != 0)
        {
            // line gives warning, but if you change the constant the warning goes away, so the warning is useful
            Console.WriteLine("WARNING: screensize doesn't match blocksize");
        }

        TileType[,] _tiles = new TileType[GridSizeY, GridSizeX * 8];

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

        int index = tiles.GetLength(0) - idY;
        System.Console.WriteLine(index + " " + morio.y);

        // check horizontal collisions
        if (morio.x % BlockSize != 0) // If Morio is in only one tile there is no need to check collisions
        {
            // horizontal collisions stuff
        }

        // Check vertical collisions with tiles
        if (morio.y % BlockSize != 0) // If Morio is in only one tile there is no need to check collisions
        {
            // Vertical collisions stuff
           
            /*
            if (tiles[tiles.GetLength(0) - idY, idX].GetHashCode() != -1) // if inside of solid block
            {
                // move morio down
                //morio.y = WindowHeight - (tiles.GetLength(0) - idY - 1) * BlockSize;
                System.Threading.Thread.Sleep(5000);
            }
           */ 

            if (tiles[index, idX].GetHashCode() != -1) // if inside of solid block
            {
                // move morio up
                //morio.y = idY * BlockSize;
                morio.y = 4f * BlockSize;
                morio.SetGrounded();
            }
        }
    }

    public void Render()
    {
        // Render background
        if (!debugModeEnabled)
        {
            Rectangle src = new(0, 0, 512f, backgroundTex.Height);
            float backTexSpeed = 0.3f;
            Rectangle dest = new(WindowWidth * 0.5f - backTexSpeed * morio.x, 0, WindowWidth, WindowHeight);
            DrawTexturePro(backgroundTex, src, dest, new(0, 0), 0, Color.RayWhite);

            if (morio.x > 0.5 * BlockSize * tiles.GetLength(1))
            {
                dest.X += WindowWidth;
                DrawTexturePro(backgroundTex, src, dest, new(0, 0), 0, Color.RayWhite);
            }
            else if (morio.x < 0.5 * BlockSize * tiles.GetLength(1))
            {
                dest.X -= WindowWidth;
                DrawTexturePro(backgroundTex, src, dest, new(0, 0), 0, Color.RayWhite);
            }
        }

        for (int j = 0; j < tiles.GetLength(0); j++) // y
        {
            for (int i = 0; i < tiles.GetLength(1); i++) // x
            {
                if (tiles[j, i].GetHashCode() == -1)
                {
                    continue;
                }

                Vector2 pos = new(i * BlockSize - morio.x, j * BlockSize);
                Rectangle src = blockTexSourceRects[tiles[j, i].GetHashCode()];
                Rectangle dest = new(pos, BlockSize + 1, BlockSize);

                DrawTexturePro(blocksTex, src, dest, new(0, 0), 0, Color.RayWhite);
            }
        }

        morio.Render();

        // Draw gridlines for debugging purposes, do not remove
        if (debugModeEnabled)
        {
            DrawFPS(10, 10);

            for (int i = 0; i < GridSizeY; i++)
            {
                int LineY = i * (int)BlockSize;
                // Console.WriteLine(LineY);
                DrawLine(0, LineY, WindowWidth, LineY, Color.Black);

            }
            for (int i = 0; i < GridSizeX + 2; i++)
            {
                int LineX = i * (int)BlockSize - (int)morio.x % (int)BlockSize;
                DrawLine(LineX, 0, LineX, WindowHeight, Color.Black);
            }

            morio.RenderDebugInfo();
        }
    }
}
