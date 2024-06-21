public static class Constants
{
    // useful because it makes resizing the window possible without manually adjusting the block size, default = 1f
    static float const_mult = 0.8f;

    public static int WindowWidth = (int)(1800 * const_mult);
    public static int WindowHeight = (int)(1020 * const_mult);
    public static float BlockSize = (int)(60 * const_mult);

    public static bool RenderDebugStuff = true;
}