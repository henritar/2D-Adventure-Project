using Assets.Scripts.Runtime.Shared.Interfaces.Player;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerInteractor : MonoBehaviour
    {
        public IInteractable Current { get; private set; }

        public event Action<IInteractable> InteractableEntered;
        public event Action InteractableExited;
        void OnTriggerEnter2D(Collider2D other)
        {
            Current = other.GetComponent<IInteractable>();

            if (Current != null)
            {
                Debug.Log($"Player can interact with {other.gameObject.name}");
                InteractableEntered?.Invoke(Current);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<IInteractable>() == Current)
            {
                Current = null;
                InteractableExited?.Invoke();
            }
        }

        public bool CanInteract => Current != null && !Current.IsBusy;

        public void Interact(PlayerCharacterController player)
        {
            Current?.Interact(player);
        }
    }

}
