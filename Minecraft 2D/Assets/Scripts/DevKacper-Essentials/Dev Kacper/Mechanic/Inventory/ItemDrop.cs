using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DevKacper.Mechanic
{
    public class ItemDrop : MonoBehaviour, IDropHandler
    {
        protected ItemDrag itemDrag;

        protected virtual void Start()
        {
            itemDrag = GetComponent<ItemDrag>();
        }


        public virtual void OnDrop(PointerEventData eventData) { }
    }
}


