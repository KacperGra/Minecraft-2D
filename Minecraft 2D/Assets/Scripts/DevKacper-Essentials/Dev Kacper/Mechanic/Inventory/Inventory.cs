using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DevKacper.Mechanic
{
    public abstract class Inventory
    {
        public UnityEvent OnInventoryUpdated = new UnityEvent();

        protected Dictionary<int, Slot> slots;

        public Inventory(int size)
        {
            slots = new Dictionary<int, Slot>();
            for(int i = 0; i < size; ++i)
            {
                slots.Add(i, new Slot { item = null, amount = 0, id = i});
            }
        }

        public virtual void SetSize(int size)
        {
            slots = new Dictionary<int, Slot>();
            for (int i = 0; i < size; ++i)
            {
                slots.Add(i, new Slot { item = null, amount = 0, id = i });
            }
        }

        public Item GetItem(int id)
        {
            if(slots.ContainsKey(id))
            {
                return slots[id].item;
            }
            return null;
        }

        public Slot GetSlot(int id)
        {
            return slots[id];
        }

        public abstract void SetItem(Item item, int id);

        public abstract void RemoveItem(Item item);

        public abstract void AddItem(Item item);

        public abstract void AddItems(List<Item> items);

        public abstract void Swap(int firstItemID, int secondItemID);

        public Dictionary<int, Slot> GetSlots()
        {
            return slots;
        }
    }
}


