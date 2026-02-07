using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Gameplay.Player;
using Assets.Scripts.Runtime.Shared;
using UnityEngine;

namespace Assets.Scripts.Runtime.Manager.States.Player
{
    public class InteractCharacterState : BaseState<CharacterStateEnum>
    {
        protected override CharacterStateEnum CurrentState => CharacterStateEnum.Interact;

        private PlayerCharacterController _characterController;

        public InteractCharacterState(PlayerCharacterController characterController)
        {
            _characterController = characterController;
        }

        protected override void OnEnterState()
        {
            Debug.Log("Entering Interact Character State");
        }

        protected override void OnExitState()
        {
            Debug.Log("Exiting Interact Character State");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnFixedUpdate()
        {
        }
    }
}
