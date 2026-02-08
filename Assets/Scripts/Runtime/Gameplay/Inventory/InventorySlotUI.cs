using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.Gameplay.Inventory
{
    public class InventorySlotUI : MonoBehaviour, IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler,
    IPointerClickHandler
    {
        public Image icon;
        public TMP_Text itemText;
        public Image highlightBorder;

        public event Action<int> Clicked;
        public event Action<int> BeginDrag;
        public event Action<int, PointerEventData> Drag;
        public event Action<int> EndDrag;
        public event Action<int> Drop;

        public int Index { get; private set; }

        public void Init(int index)
        {
            Index = index;
        }

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
                itemText.text = $"{slot.item.itemName}{quantityString}";
            }
        }

        public void SetHighlighted(bool value)
        {
            highlightBorder.enabled = value;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDrag?.Invoke(Index);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Drag?.Invoke(Index, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDrag?.Invoke(Index);
        }

        public void OnDrop(PointerEventData eventData)
        {
            Drop?.Invoke(Index);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            Clicked?.Invoke(Index);
        }
    }

}
