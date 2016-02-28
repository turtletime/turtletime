using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheloniiUnity
{
    /// <summary>
    /// Game-wide constant and read-only values.
    /// </summary>
    static class Constants
    {
        public const float PARTICLE_RADIUS = 0.03f;
        public static StringCollection STRINGS = new StringCollection("strings.txt");
        public static bool DEBUG_MODE = true;
        public const float PIXELS_PER_UNIT = 100.0f;
    }
}
