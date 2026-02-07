using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Gameplay.Player;
using Assets.Scripts.Runtime.Shared;
using UnityEngine;

namespace Assets.Scripts.Runtime.Manager.States.Player
{
    internal class MoveCharacterState : BaseState<CharacterStateEnum>
    {
        protected override CharacterStateEnum CurrentState => CharacterStateEnum.Move;

        private PlayerCharacterInputManager _inputManager;
        private PlayerCharacterController _characterController;

        public MoveCharacterState(PlayerCharacterInputManager inputManager, PlayerCharacterController characterController)
        {
            _inputManager = inputManager;
            _characterController = characterController;
        }

        protected override void OnEnterState()
        {
            //Debug.Log("Entering Move Character State");
        }

        protected override void OnExitState()
        {
            _characterController.OnMove(Vector2.zero);
            //Debug.Log("Exiting Move Character State");
        }

        protected override void OnUpdate()
        {
            _characterController.OnMove(_inputManager.MoveInput);
        }

        protected override void OnFixedUpdate()
        {
        }

    }
}
