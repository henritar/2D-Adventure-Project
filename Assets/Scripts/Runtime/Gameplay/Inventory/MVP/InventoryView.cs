using Assets.Scripts.Runtime.Shared.Interfaces.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Inventory.MVP
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        public InventorySlotUI slotPrefab;
        public Transform gridParent;

        private List<InventorySlotUI> slots = new();

        public event Action<int> SlotClicked;

        public void Build(int slotCount)
        {
            foreach (Transform child in gridParent)
                Destroy(child.gameObject);

            slots.Clear();

            for (int i = 0; i < slotCount; i++)
            {
                var ui = Instantiate(slotPrefab, gridParent);
                slots.Add(ui);
                ui.Clicked += () => SlotClicked?.Invoke(i);
            }
        }

        public void SetSlot(int index, InventorySlot slot)
        {
            slots[index].Set(slot);
        }

        public void ClearSlot(int index)
        {
            slots[index].Set(new InventorySlot());
        }
    }
}
