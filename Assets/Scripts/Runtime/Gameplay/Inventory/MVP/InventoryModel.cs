using Assets.Scripts.Runtime.Gameplay.Inventory.ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Inventory.MVP
{
    public class InventoryModel
    {
        public IReadOnlyList<InventorySlot> Slots => _slots;

        private readonly List<InventorySlot> _slots;
        public event Action OnChanged;

        public InventoryModel(int size)
        {
            _slots = new List<InventorySlot>(size);
            for (int i = 0; i < size; i++)
                _slots.Add(new InventorySlot());
        }

        public bool AddItem(ItemData item, int amount)
        {
            foreach (var slot in _slots)
            {
                if (slot.item == item && item.stackable && slot.quantity < item.maxStack)
                {
                    slot.quantity += amount;
                    OnChanged?.Invoke();
                    return true;
                }
            }

            foreach (var slot in _slots)
            {
                if (slot.IsEmpty)
                {
                    slot.item = item;
                    slot.quantity = amount;
                    OnChanged?.Invoke();
                    return true;
                }
            }

            return false;
        }

        public void Swap(int a, int b)
        {
            var temp = _slots[a];
            _slots[a] = _slots[b];
            _slots[b] = temp;

            OnChanged?.Invoke();
        }

        public void UseItem(int index)
        {
            var slot = _slots[index];

            if (slot.IsEmpty)
                return;

            Debug.Log($"{slot.item.itemName} used!");

            slot.quantity--;

            if (slot.quantity <= 0)
                slot.Clear();

            OnChanged?.Invoke();
        }
    }

}
