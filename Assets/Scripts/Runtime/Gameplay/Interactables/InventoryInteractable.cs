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
        [SerializeField] 
        private Transform interactionAnchor;
        public string InteractionText => "Hold E to pick up";
        public bool IsBusy => false;

        public Transform InteractionAnchor => interactionAnchor;

        public void Interact(PlayerCharacterController player)
        {
            Debug.Log($"Added {itemData.name} to inventory.");
            player.AddItemToInventory(itemData, 1);
            Destroy(gameObject);
        }
    }
}
