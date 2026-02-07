using Assets.Scripts.Runtime.Shared.Interfaces.Inventory;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Inventory.MVP
{
    public class InventoryPresenter
    {
        private readonly InventoryModel model;
        private readonly IInventoryView view;

        public InventoryPresenter(InventoryModel model, IInventoryView view)
        {
            this.model = model;
            this.view = view;

            view.Build(model.Slots.Count);
            view.SlotClicked += OnSlotClicked;
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
    }
}
