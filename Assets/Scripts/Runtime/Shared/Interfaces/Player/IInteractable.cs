using Assets.Scripts.Runtime.Gameplay.Player;
using UnityEngine;

namespace Assets.Scripts.Runtime.Shared.Interfaces.Player
{
    public interface IInteractable
    {
        void Interact(PlayerCharacterController player);
        bool IsBusy { get; }
        string InteractionText { get; }
        Transform InteractionAnchor { get; }
    }
}
