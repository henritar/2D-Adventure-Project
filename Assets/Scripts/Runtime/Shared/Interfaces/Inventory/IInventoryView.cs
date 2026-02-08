using Assets.Scripts.Runtime.Gameplay.Inventory;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Shared.Interfaces.Inventory
{
    public interface IInventoryView
    {
        public event Action<int> SlotClicked;
        event Action<int> BeginDrag;
        event Action<int> EndDrag;
        event Action<int> Drop;

        void ShowDragIcon(Sprite icon);
        void MoveDragIcon(Vector2 position);
        void HideDragIcon();
        void Build(int slotCount);
        void SetSlot(int index, InventorySlot slot);
        void ClearSlot(int index);
        void HighlightSlot(int index);
        void ClearHighlight();
    }
}
