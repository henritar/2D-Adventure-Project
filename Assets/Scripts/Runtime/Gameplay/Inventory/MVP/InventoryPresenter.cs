using Assets.Scripts.Runtime.Shared.Interfaces.Inventory;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Inventory.MVP
{
    public class InventoryPresenter
    {
        private readonly InventoryModel model;
        private readonly IInventoryView view;

        private int draggedIndex = -1;

        public InventoryPresenter(InventoryModel model, IInventoryView view)
        {
            this.model = model;
            this.view = view;

            view.Build(model.Slots.Count);
            view.SlotClicked += OnSlotClicked;
            view.BeginDrag += OnBeginDrag;
            view.EndDrag += OnEndDrag;
            view.Drop += OnDrop;
            model.OnChanged += Refresh;
            Refresh();
        }

        private void Refresh()
        {
            for (int i = 0; i < model.Slots.Count; i++)
            {
                var slot = model.Slots[i];

                if (slot.IsEmpty)
                    view.ClearSlot(i);
                else
                    view.SetSlot(i, slot);
            }
        }

        private void OnSlotClicked(int index)
        {
            //model.UseItem(index);
            Debug.Log($"Clicked slot {index}");
        }

        private void OnBeginDrag(int index)
        {
            if (model.Slots[index].IsEmpty)
                return;

            draggedIndex = index;
            view.ShowDragIcon(model.Slots[index].item.icon);
        }

        private void OnEndDrag(int _)
        {
            draggedIndex = -1;
            view.HideDragIcon();
        }

        private void OnDrop(int targetIndex)
        {
            if (draggedIndex == -1 || draggedIndex == targetIndex)
                return;

            model.Swap(draggedIndex, targetIndex);
        }
    }
}
