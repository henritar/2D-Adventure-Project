using Assets.Scripts.Runtime.Gameplay.Player;

namespace Assets.Scripts.Runtime.Shared.Interfaces.Player
{
    public interface IInteractable
    {
        void Interact(PlayerCharacterController player);
        bool IsBusy { get; }
    }
}
