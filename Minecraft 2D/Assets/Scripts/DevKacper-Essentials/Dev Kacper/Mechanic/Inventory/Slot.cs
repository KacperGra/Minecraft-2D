using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevKacper.Mechanic
{
    public class Slot
    {
        public int id;
        public Item item;
        public int amount;

        public void Clear()
        {
            item = null;
            amount = 0;
        }
    }
}

