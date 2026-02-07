using Assets.Scripts.Runtime.Gameplay.Inventory.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        public ItemData item;
        public int quantity;

        public bool IsEmpty => item == null;

        public void Clear()
        {
            item = null;
            quantity = 0;
        }
    }
}
