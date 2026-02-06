using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Shared;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Runtime.Manager.States.MainGame
{
    public class PlayingGameState : BaseGameState
    {
        public Transform playerTransform;
        public InputSystem_Actions inputActions = new InputSystem_Actions();
        
        private Vector2 movementInput;

        protected override GameStatesEnum GameState => GameStatesEnum.Playing;

        protected override void OnEnterState()
        {
            Debug.Log("Entering Playing Game State");

            OnEnable();
        }

        protected override void OnExitState()
        {
            OnDisable();
            Debug.Log("Exiting Playing Game State");
        }

        protected override void OnUpdate()
        {
            playerTransform.Translate(new Vector3(movementInput.x, movementInput.y, 0) * Time.deltaTime * 5f);
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
            movementInput = ctx.ReadValue<Vector2>();
        }
    }
}
