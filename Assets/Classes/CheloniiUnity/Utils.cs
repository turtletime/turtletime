using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheloniiUnity
{
    /// <summary>
    /// A collection of utility methods.
    /// </summary>
    static class Utils
    {
        public static Vector2 ToAngular(Vector2 cartesian)
        {
            return new Vector2(cartesian.magnitude, Vector2.Angle(Vector2.zero, cartesian) * Mathf.Deg2Rad);
        }

        public static Vector2 ToCartesian(Vector2 angular)
        {
            return new Vector2(angular.x * Mathf.Cos(angular.y), angular.x * Mathf.Sin(angular.y));
        }

        public static T ParseEnumValue<T>(String input)
        {
            return (T)Enum.Parse(typeof(T), input, true);
        }
    }
}
