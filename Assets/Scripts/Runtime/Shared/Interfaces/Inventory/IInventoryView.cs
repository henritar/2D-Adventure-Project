using Assets.Scripts.Runtime.Gameplay.Inventory;
using System;

namespace Assets.Scripts.Runtime.Shared.Interfaces.Inventory
{
    public interface IInventoryView
    {
        public event Action<int> SlotClicked;
        void Build(int slotCount);
        void SetSlot(int index, InventorySlot slot);
        void ClearSlot(int index);
    }
}
