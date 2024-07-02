using System.Numerics;
using Raylib_cs;

// not sure why namespace is needed here, but removing it gives warning
namespace Constants_name
{
    public static class Constants
    {
        // useful because it makes resizing the window possible without manually adjusting the block size, default = 1f
        const float constMult = 0.8f;

        public const int TargetFps = 144;
        public const int WindowWidth = (int)(1800 * constMult);
        public const int WindowHeight = (int)(1020 * constMult);
        public const float BlockSize = (int)(60 * constMult);

        static Vector2 texSize = new(16f, 16f);
        public static readonly Rectangle[] blockTexSourceRects = {
            // ground
            new(0f, 0f, texSize),
            new(17f, 0f, texSize),
            new(34f, 0f, texSize),
            new(0f, 17f, texSize),
            new(17f, 17f, texSize),
            new(34f, 17f, texSize),
            new(0f, 34f, texSize),
            new(17f, 34f, texSize),
            new(34f, 34f, texSize),
            // platform
            new(51f, 0, texSize),
            new(68f, 0, texSize),
            new(102, 0, texSize),
        };
    }
}
