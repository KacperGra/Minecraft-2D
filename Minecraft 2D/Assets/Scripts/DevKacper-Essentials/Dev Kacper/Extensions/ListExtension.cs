using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevKacper.Extensions
{
    public static class ListExtension
    {
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            if(list == null || list.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}

