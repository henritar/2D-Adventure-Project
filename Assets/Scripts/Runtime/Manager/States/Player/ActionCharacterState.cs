using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Gameplay.Player;
using Assets.Scripts.Runtime.Shared;
using UnityEngine;

namespace Assets.Scripts.Runtime.Manager.States.Player
{
    internal class ActionCharacterState : BaseState<CharacterStateEnum>
    {
        protected override CharacterStateEnum CurrentState => CharacterStateEnum.Action;

        private PlayerCharacterInputManager _inputManager;
        private PlayerCharacterController _characterController;

        public ActionCharacterState(PlayerCharacterInputManager inputManager, PlayerCharacterController characterController)
        {
            _inputManager = inputManager;
            _characterController = characterController;
        }

        protected override void OnEnterState()
        {
            Debug.Log("Entering Action Character State");
        }

        protected override void OnExitState()
        {
            _characterController.OnMove(Vector2.zero);
            Debug.Log("Exiting Action Character State");
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
