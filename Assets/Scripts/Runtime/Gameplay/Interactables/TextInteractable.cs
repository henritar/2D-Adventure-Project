using Assets.Scripts.Runtime.Gameplay.Player;
using Assets.Scripts.Runtime.Shared.Interfaces.Player;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Interactables
{
    internal class TextInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private Transform interactionAnchor;
        [SerializeField]
        private string TextToDisplay;
        public bool IsBusy => false;

        public string InteractionText => TextToDisplay;

        public Transform InteractionAnchor => interactionAnchor;
        public void Interact(PlayerCharacterController player)
        {
            Debug.Log($"Player has interacted with {name}!");
        }
    }
}
