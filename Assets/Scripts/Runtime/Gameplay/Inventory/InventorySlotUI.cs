using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.Gameplay.Inventory
{
    public class InventorySlotUI : MonoBehaviour
    {
        public Image icon;
        public TMP_Text quantityText;

        public event Action Clicked;

        public void Set(InventorySlot slot)
        {
            if (slot.IsEmpty)
            {
                icon.enabled = false;
                quantityText.text = "";
            }
            else
            {
                icon.enabled = true;
                icon.sprite = slot.item.icon;
                quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : "";
            }
        }

        public void OnClick()
        {
            Clicked?.Invoke();
        }
    }

}
