using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevKacper.Extensions
{
    public static class VectorExtension
    {
        public static Vector3 WithValues(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
        }

        public static Vector2 WithValues(this Vector2 original, float? x = null, float? y = null)
        {
            return new Vector2(x ?? original.x, y ?? original.y);
        }

        public static Vector2 ToVector2(this Vector3 original)
        {
            return new Vector2(original.x, original.y);
        }

        public static Vector3 ToVector3(this Vector2 original)
        {
            return new Vector3(original.x, original.y, 0f);
        }
    }
}

