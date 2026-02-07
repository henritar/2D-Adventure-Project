using Assets.Scripts.Runtime.Shared.Interfaces.Player;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerInteractor : MonoBehaviour
    {
        private IInteractable current;

        void OnTriggerEnter2D(Collider2D other)
        {
            current = other.GetComponent<IInteractable>();

            if (current != null)
            {
                Debug.Log($"Player can interact with {other.gameObject.name}");
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<IInteractable>() == current)
                current = null;
        }

        public bool CanInteract => current != null && !current.IsBusy;

        public void Interact(PlayerCharacterController player)
        {
            current?.Interact(player);
        }
    }

}
