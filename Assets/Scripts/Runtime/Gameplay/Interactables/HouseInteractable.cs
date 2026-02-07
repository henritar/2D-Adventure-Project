using Assets.Scripts.Runtime.Gameplay.Player;
using Assets.Scripts.Runtime.Shared.Interfaces.Player;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Interactables
{
    public class HouseInteractable : MonoBehaviour, IInteractable
    {
        public bool IsBusy => false;

        public void Interact(PlayerCharacterController player)
        {
            Debug.Log("Player has interacted with the house!");
        }
    }
}
