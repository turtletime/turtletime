using UnityEngine;
using SimpleJSON;

namespace TurtleTime
{
    static class TurtleUtils
    {
        public static readonly float CAFE_COORDINATE_SCALE = 0.25f;
        public static readonly Vector2 CAFE_COORDINATE_OFFSET = new Vector2(0.5f, 0.5f);

        public static Vector3 CafeSpaceToWorldCoordinates(Vector2 cafeCoordinates)
        {
            return CafeSpaceToWorldCoordinates(cafeCoordinates, 0);
        }

        public static Vector3 CafeSpaceToWorldCoordinates(Vector3 cafeCoordinates)
        {
            return new Vector3(CAFE_COORDINATE_SCALE * cafeCoordinates.x + CAFE_COORDINATE_OFFSET.x, CAFE_COORDINATE_SCALE * cafeCoordinates.z, CAFE_COORDINATE_SCALE * cafeCoordinates.y + CAFE_COORDINATE_OFFSET.y);
        }

        public static Vector3 CafeSpaceToWorldCoordinates(Vector2 cafeCoordinates, float y)
        {
            return new Vector3(CAFE_COORDINATE_SCALE * cafeCoordinates.x + CAFE_COORDINATE_OFFSET.x, y, CAFE_COORDINATE_SCALE * cafeCoordinates.y + CAFE_COORDINATE_OFFSET.y);
        }

        public static Vector3 WorldSpacePointToCafeCoordinates(Vector3 worldCoordinates)
        {
            return new Vector3(
                (worldCoordinates.x - CAFE_COORDINATE_OFFSET.x) / CAFE_COORDINATE_SCALE,
                (worldCoordinates.z - CAFE_COORDINATE_OFFSET.y) / CAFE_COORDINATE_SCALE,
                worldCoordinates.y / CAFE_COORDINATE_SCALE);
        }

        public static Vector3 WorldSpaceVectorToCafeCoordinates(Vector3 worldCoordinates)
        {
            return new Vector3(
                worldCoordinates.x / CAFE_COORDINATE_SCALE,
                worldCoordinates.z / CAFE_COORDINATE_SCALE,
                worldCoordinates.y / CAFE_COORDINATE_SCALE);
        }
    }
}
