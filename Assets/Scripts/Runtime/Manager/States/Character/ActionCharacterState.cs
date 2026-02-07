using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Shared;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Runtime.Manager.States.Character
{
    internal class ActionCharacterState : BaseState<CharacterStateEnum>
    {
        protected override CharacterStateEnum CurrentState => CharacterStateEnum.Action;

        private Vector2 _movementInput;
        private InputSystem_Actions inputActions = new InputSystem_Actions();

        public Transform CharacterTransform;

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
            CharacterTransform.Translate(new Vector3(_movementInput.x, _movementInput.y, 0) * Time.deltaTime * 5f);
        }

        protected override void OnFixedUpdate()
        {
        }

        void OnEnable()
        {
            inputActions.Player.Enable();
            inputActions.Player.Move.performed += OnMove;
            inputActions.Player.Move.canceled += OnMove;
        }

        void OnDisable()
        {
            inputActions.Player.Disable();
            inputActions.Player.Move.performed -= OnMove;
            inputActions.Player.Move.canceled -= OnMove;
        }

        void OnMove(InputAction.CallbackContext ctx)
        {
            _movementInput = ctx.ReadValue<Vector2>();
        }
    }
}
