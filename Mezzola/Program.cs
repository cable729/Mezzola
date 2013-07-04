using System;

namespace Mezzola
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MezzolaGame()) game.Run();
        }
    }
#endif
}
