using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevKacper.Mechanic
{
    public abstract class Item : ScriptableObject
    {
        [HideInInspector]
        [SerializeField] protected int id;
        public int ID => id;

        protected string itemName;
        public string Name => itemName;
    }
}
