using Assets.Scripts.Runtime.Gameplay.Inventory.ScriptableObjects;
using Assets.Scripts.Runtime.Gameplay.Player;
using Assets.Scripts.Runtime.Shared.Interfaces.Player;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Interactables
{
    public class InventoryInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private ItemData itemData;
        public bool IsBusy => false;

        public void Interact(PlayerCharacterController player)
        {
            player.AddItemToInventory(itemData, 1);
        }
    }
}
