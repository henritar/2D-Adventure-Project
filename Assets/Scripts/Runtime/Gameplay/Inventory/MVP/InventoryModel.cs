using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Gameplay.Inventory.ScriptableObjects;
using Assets.Scripts.Runtime.Manager;
using Assets.Scripts.Runtime.Systems.Save.DTO;
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
                if (slot.item == item && item.Stackable && slot.quantity < item.MaxStack)
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

            SoundManager.Instance.PlaySFX(SoundsEnum.UseItem);
            Debug.Log($"{slot.item.ItemName} used!");

            slot.quantity--;

            if (slot.quantity <= 0)
                slot.Clear();

            OnChanged?.Invoke();
        }

        public InventorySaveData GetSaveData()
        {
            var data = new InventorySaveData();

            foreach (var slot in _slots)
            {
                if (slot.IsEmpty)
                {
                    data.slots.Add(new InventorySlotSaveData());
                }
                else
                {
                    data.slots.Add(new InventorySlotSaveData
                    {
                        itemId = slot.item.ItemId,
                        quantity = slot.quantity
                    });
                }
            }

            return data;
        }

        public void LoadFromSave(InventorySaveData data, ItemDatabase itemDatabase)
        {
            _slots.Clear();

            foreach (var slotData in data.slots)
            {
                if (string.IsNullOrEmpty(slotData.itemId))
                {
                    _slots.Add(new InventorySlot());
                    continue;
                }

                ItemData item = itemDatabase.GetById(slotData.itemId);

                _slots.Add(new InventorySlot
                {
                    item = item,
                    quantity = slotData.quantity
                });
            }

            OnChanged?.Invoke();
        }
    }
}
