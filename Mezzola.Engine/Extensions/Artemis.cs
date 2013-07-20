using System;
using Artemis;

namespace Mezzola.Engine.Extensions
{
    public static class Artemis
    {
        private static long previousTick;
        private static float deltaToSeconds;

        public static float DeltaToSeconds(this EntityWorld world)
        {
            if (world.Delta != previousTick)
            {
                previousTick = world.Delta;
                deltaToSeconds = (float)TimeSpan.FromTicks(world.Delta).TotalSeconds;
            }
            return deltaToSeconds;
        }
    }
}
