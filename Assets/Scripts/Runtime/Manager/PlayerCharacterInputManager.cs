using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Runtime.Manager
{
    public class PlayerCharacterInputManager : MonoBehaviour
    {
        public event Action<bool> MoveInputAction;
        public event Action<bool> AttackPressedAction;
        public event Action<bool> InteractPressedAction;

        public Vector2 MoveInput { get; private set; }
        public bool IsAttacking { get; private set; }
        public bool IsInteracting { get; private set; }

        private InputSystem_Actions input;

        void Awake()
        {
            input = new InputSystem_Actions();
            DontDestroyOnLoad(this);
        }

        void OnEnable()
        {
            input.Player.Enable();
            input.Player.Move.performed += OnMove;
            input.Player.Move.canceled += OnMove;
            input.Player.Interact.performed += OnInteract;
            input.Player.Interact.canceled += OnInteract;
            input.Player.Attack.performed += OnAttack;
            input.Player.Attack.canceled += OnAttack;
        }

        void OnDisable()
        {
            input.Player.Disable();
        }

        void OnMove(InputAction.CallbackContext ctx)
        {
            MoveInput = ctx.ReadValue<Vector2>();
            MoveInputAction?.Invoke(MoveInput != Vector2.zero);
        }

        void OnInteract(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();
            IsInteracting = value > 0.5f;

            InteractPressedAction(IsInteracting);
        }

        void OnAttack(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();
            IsAttacking = value > 0.5f;

            AttackPressedAction(IsAttacking);
        }
    }
}
