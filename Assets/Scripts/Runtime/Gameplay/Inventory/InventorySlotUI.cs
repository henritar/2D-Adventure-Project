using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.Gameplay.Inventory
{
    public class InventorySlotUI : MonoBehaviour
    {
        public Image icon;
        public TMP_Text itemText;

        public event Action Clicked;

        public void Set(InventorySlot slot)
        {
            if (slot.IsEmpty)
            {
                icon.enabled = false;
                itemText.text = "";
            }
            else
            {
                icon.enabled = true;
                icon.sprite = slot.item.icon;
                string quantityString = slot.quantity > 1 ? $" x{slot.quantity.ToString()}" : "";
                itemText.text = $"{slot.item.name}{quantityString}";
            }
        }

        public void OnClick()
        {
            Clicked?.Invoke();
        }
    }

}
