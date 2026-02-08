using Assets.Scripts.Runtime.Gameplay.Player;
using Assets.Scripts.Runtime.Shared.Interfaces.Player;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Interactables
{
    public class HouseInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] 
        private Transform interactionAnchor;
        public bool IsBusy => false;

        public string InteractionText => "I'm a house!";

        public Transform InteractionAnchor => interactionAnchor;
        public void Interact(PlayerCharacterController player)
        {
            Debug.Log("Player has interacted with the house!");
        }
    }
}
