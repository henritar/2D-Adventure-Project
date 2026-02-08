using Assets.Scripts.Runtime.Shared.Interfaces.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.Gameplay.Inventory.MVP
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        public InventorySlotUI slotPrefab;
        public Transform gridParent;
        public Image dragIcon;

        public event Action<int> BeginDrag;
        public event Action<int> EndDrag;
        public event Action<int> Drop;
        public event Action<int> SlotClicked;

        private List<InventorySlotUI> slots = new();

        public void Build(int slotCount)
        {
            foreach (Transform child in gridParent)
                Destroy(child.gameObject);

            slots.Clear();

            for (int i = 0; i < slotCount; i++)
            {
                var ui = Instantiate(slotPrefab, gridParent);
                slots.Add(ui);
                ui.Init(i);
                ui.Clicked += () => SlotClicked?.Invoke(i);
                ui.BeginDrag += index => BeginDrag?.Invoke(index);
                ui.Drag += (index, pointerEvent) => MoveDragIcon(pointerEvent.position);
                ui.EndDrag += index => EndDrag?.Invoke(index);
                ui.Drop += index => Drop?.Invoke(index);
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

        public void ShowDragIcon(Sprite icon)
        {
            dragIcon.sprite = icon;
            dragIcon.gameObject.SetActive(true);
        }

        public void MoveDragIcon(Vector2 position)
        {
            dragIcon.transform.position = position;
        }

        public void HideDragIcon()
        {
            dragIcon.gameObject.SetActive(false);
        }
    }
}
