using Assets.Scripts.Runtime.Shared.Interfaces.Inventory;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.Gameplay.Inventory.MVP
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        public InventorySlotUI SlotPrefab;
        public Transform GridParent;
        public Image DragIcon;

        public TMP_Text ItemName;
        public TMP_Text ItemDescription;
        public Button ConfirmButton;

        public event Action<int> BeginDrag;
        public event Action<int> EndDrag;
        public event Action<int> Drop;
        public event Action<int> SlotClicked;

        private List<InventorySlotUI> slots = new();

        private int highlightedIndex = -1;

        public void Build(int slotCount)
        {
            //TODO: Implement object pooling for better performance
            foreach (Transform child in GridParent)
                Destroy(child.gameObject);

            slots.Clear();

            for (int i = 0; i < slotCount; i++)
            {
                var ui = Instantiate(SlotPrefab, GridParent);
                slots.Add(ui);
                ui.Init(i);
                //TODO: Consider using a more efficient event system if performance becomes an issue
                ui.Clicked += (index) => SlotClicked?.Invoke(index);
                ui.BeginDrag += index => BeginDrag?.Invoke(index);
                ui.Drag += (index, pointerEvent) => MoveDragIcon(pointerEvent.position);
                ui.EndDrag += index => EndDrag?.Invoke(index);
                ui.Drop += index => Drop?.Invoke(index);
            }

            DragIcon.gameObject.SetActive(false);
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
            DragIcon.sprite = icon;
            DragIcon.gameObject.SetActive(true);
        }

        public void MoveDragIcon(Vector2 position)
        {
            DragIcon.transform.position = position;
        }

        public void HideDragIcon()
        {
            DragIcon.gameObject.SetActive(false);
        }

        public void HighlightSlot(int index)
        {
            ClearHighlight();
            slots[index].SetHighlighted(true);
            highlightedIndex = index;
        }

        public void ClearHighlight()
        {
            if (highlightedIndex != -1)
                slots[highlightedIndex].SetHighlighted(false);

            highlightedIndex = -1;
        }
    }
}
