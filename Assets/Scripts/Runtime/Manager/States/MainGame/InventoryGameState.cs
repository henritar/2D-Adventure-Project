using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Gameplay.Inventory.MVP;
using Assets.Scripts.Runtime.Shared;
using Assets.Scripts.Runtime.Shared.Interfaces.Inventory;
using UnityEngine;

namespace Assets.Scripts.Runtime.Manager.States.MainGame
{
    internal class InventoryGameState : BaseState<GameStatesEnum>
    {
        private InventoryView _inventoryView;
        private PlayerCharacterInputManager _playerCharacterInputManager;
        protected override GameStatesEnum CurrentState => GameStatesEnum.Inventory;

        public InventoryGameState(PlayerCharacterInputManager playerCharacterInputManager, InventoryView inventoryView)
        {
            _playerCharacterInputManager = playerCharacterInputManager;
            _inventoryView = inventoryView;
        }

        protected override void OnEnterState()
        {
            Debug.Log("Entering Inventory Game State");
            OnEnable();
            _inventoryView.gameObject.SetActive(true);
        }

        protected override void OnExitState()
        {
            OnDisable();
            _inventoryView.gameObject.SetActive(false);
            Debug.Log("Exiting Inventory Game State");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnFixedUpdate()
        {
        }

        void OnEnable()
        {
            _playerCharacterInputManager.ToggleUIInputManager(true);
            _playerCharacterInputManager.InventoryPressedAction += OnCancelPressed;
            _inventoryView.ExitButtonClicked += OnExitButtonPressed;
        }

        void OnDisable()
        {
            _inventoryView.ExitButtonClicked -= OnExitButtonPressed;
            _playerCharacterInputManager.InventoryPressedAction -= OnCancelPressed;
            _playerCharacterInputManager.ToggleUIInputManager(false);
        }

        void OnExitButtonPressed()
        {
            OnCancelPressed(true);
        }

        void OnCancelPressed(bool isPressed)
        {
            if (isPressed)
            {
                _stateManager.ChangeState(GameStatesEnum.Playing);
            }
        }
    }
}
