using UnityEngine;

namespace TurtleTime
{
    static class TurtleUtils
    {
        public static readonly float CAFE_COORDINATE_SCALE = 0.5f;

        public static Vector3 ToWorldCoordinates(Vector2 cafeCoordinates)
        {
            return ToWorldCoordinates(cafeCoordinates, 0);
        }

        public static Vector3 ToWorldCoordinates(Vector2 cafeCoordinates, float y)
        {
            return new Vector3(CAFE_COORDINATE_SCALE * cafeCoordinates.x, y, CAFE_COORDINATE_SCALE * cafeCoordinates.y);
        }
    }
}
