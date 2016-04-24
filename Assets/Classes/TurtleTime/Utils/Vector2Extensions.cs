using UnityEngine;

namespace TurtleTime
{
    /// <summary>
    /// Extension methods for Vector2 objects
    /// </summary>
    static class Vector2Extensions
    {
        /// <summary>
        /// Returns a vector with each component rounded to the nearest integer.
        /// </summary>
        /// <param name="v">The input vector.</param>
        /// <returns>A vector with integer-valued components.</returns>
        public static Vector2 RoundComponents(this Vector2 v)
        {
            return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
        }

        /// <summary>
        /// Returns the closest axis-aligned, normalized vector to the input vector.
        /// If the input is the zero-vector, the output will also be the zero-vector.
        /// Behavior for diagonal vectors (x = y or x = -y) may be unpredictable.
        /// </summary>
        /// <param name="v">The input vector.</param>
        /// <returns>A normalized vector where at least one component is zero.</returns>
        public static Vector2 AxisAlign(this Vector2 v)
        {
            if (v.x == 0 && v.y == 0)
            {
                return v;
            }
            if (v.x > 0 && v.x > Mathf.Abs(v.y))
            {
                return new Vector2(1, 0);
            }
            else if (v.x < 0 && -v.x < Mathf.Abs(v.y))
            {
                return new Vector2(-1, 0);
            }
            else if (v.y > 0)
            {
                return new Vector2(0, 1);
            }
            else
            {
                return new Vector2(0, -1);
            }
        }
    }
}