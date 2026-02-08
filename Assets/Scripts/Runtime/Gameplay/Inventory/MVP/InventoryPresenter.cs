using Assets.Scripts.Runtime.Shared.Interfaces.Inventory;

namespace Assets.Scripts.Runtime.Gameplay.Inventory.MVP
{
    public class InventoryPresenter
    {
        private readonly InventoryModel model;
        private readonly IInventoryView view;

        private int draggedIndex = -1;
        private int selectedIndex = -1;

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
            if (model.Slots[index].IsEmpty)
            {
                ClearSelection();
                return;
            }

            if (selectedIndex == -1)
            {
                Select(index);
                return;
            }

            if (selectedIndex == index)
            {
                model.UseItem(index);
                ClearSelection();
                return;
            }

            Select(index);
        }

        private void Select(int index)
        {
            selectedIndex = index;
            view.HighlightSlot(index);
            view.ShowItemDetails(model.Slots[selectedIndex].item);
        }

        private void ClearSelection()
        {
            view.ClearHighlight();
            view.ClearItemDetails();
            selectedIndex = -1;
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
            ClearSelection();
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
