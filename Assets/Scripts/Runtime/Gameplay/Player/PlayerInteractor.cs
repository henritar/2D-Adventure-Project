using Assets.Scripts.Runtime.Shared.Interfaces.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerInteractor : MonoBehaviour
    {
        public IInteractable Current { get; private set; }

        public event Action<IInteractable> InteractableChanged;

        private readonly List<IInteractable> interactablesInRange = new();

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out IInteractable interactable))
                return;

            interactablesInRange.Add(interactable);
            RecalculateCurrent();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent(out IInteractable interactable))
                return;

            interactablesInRange.Remove(interactable);
            RecalculateCurrent();
        }

        void RecalculateCurrent()
        {
            IInteractable best = null;
            float bestDistance = float.MaxValue;

            foreach (var interactable in interactablesInRange)
            {
                if (interactable.IsBusy)
                    continue;

                float distance = Vector2.Distance(
                    transform.position,
                    interactable.InteractionAnchor.position
                );

                if (distance < bestDistance)
                {
                    best = interactable;
                    bestDistance = distance;
                }
            }

            if (best == Current)
                return;

            Current = best;
            InteractableChanged?.Invoke(Current);
        }

        public bool CanInteract => Current != null;

        public void Interact(PlayerCharacterController player)
        {
            Current?.Interact(player);
        }
    }
}
