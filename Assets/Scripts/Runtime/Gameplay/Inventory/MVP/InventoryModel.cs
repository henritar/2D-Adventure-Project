using Assets.Scripts.Runtime.Gameplay.Inventory.ScriptableObjects;
using System;
using System.Collections.Generic;

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
    }

}
