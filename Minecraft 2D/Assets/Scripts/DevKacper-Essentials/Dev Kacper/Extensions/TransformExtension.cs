using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevKacper.Extensions
{
    public static class TransformExtension 
    {
        public static void DestroyChildren(this Transform transform)
        {
            foreach(Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public static void DestroyChild(this Transform transform, string name)
        {
            foreach(Transform child in transform)
            {
                if(child.name == name)
                {
                    Object.Destroy(child.gameObject);
                    return;
                }
            }
        }

        public static GameObject CreateChild(this Transform transform)
        {
            var child = new GameObject();
            return child;
        }

        public static GameObject CreateChild(this Transform transform, string name)
        {
            var child = new GameObject();
            child.name = name;
            return child;
        }

        public static GameObject CreateChild(this Transform transform, GameObject prefab)
        {
            var child = Object.Instantiate(prefab, transform);
            return child;
        }

        public static void SetLeft(this RectTransform rectTransform, float leftValue)
        {
            rectTransform.offsetMin = new Vector2(leftValue, rectTransform.offsetMin.y);
        }

        public static void SetRight(this RectTransform rectTransform, float rightValue)
        {
            rectTransform.offsetMax = new Vector2(-rightValue, rectTransform.offsetMax.y);
        }

        public static void SetTop(this RectTransform rectTransform, float topValue)
        {
            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -topValue);
        }

        public static void SetBottom(this RectTransform rectTransform, float bottomValue)
        {
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, bottomValue);
        }
    }
}

