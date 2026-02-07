using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Shared;
using UnityEngine;

namespace Assets.Scripts.Runtime.Manager.States.Character
{
    internal class ActionCharacterState : BaseState<CharacterStateEnum>
    {
        protected override CharacterStateEnum CurrentState => CharacterStateEnum.Action;
        
        private InputSystem_Actions inputActions = new InputSystem_Actions();

        public Gameplay.Character.PlayerCharacterController CharacterController;

        protected override void OnEnterState()
        {
            Debug.Log("Entering Action Character State");
            OnEnable();
        }

        protected override void OnExitState()
        {
            OnDisable();
            Debug.Log("Exiting Action Character State");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnFixedUpdate()
        {
        }

        void OnEnable()
        {
            inputActions.Player.Enable();
            inputActions.Player.Move.performed += CharacterController.OnMove;
            inputActions.Player.Move.canceled += CharacterController.OnMove;
        }

        void OnDisable()
        {
            inputActions.Player.Disable();
            inputActions.Player.Move.performed -= CharacterController.OnMove;
            inputActions.Player.Move.canceled -= CharacterController.OnMove;
        }
    }
}
