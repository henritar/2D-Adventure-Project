using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Gameplay.Inventory.MVP;
using Assets.Scripts.Runtime.Shared;
using UnityEngine;

namespace Assets.Scripts.Runtime.Manager.States.MainGame
{
    internal class InventoryGameState : BaseState<GameStatesEnum>
    {
        private InventoryView _inventoryView;
        protected override GameStatesEnum CurrentState => GameStatesEnum.Inventory;

        public InventoryGameState(InventoryView inventoryView)
        {
            _inventoryView = inventoryView;
        }

        protected override void OnEnterState()
        {
            Debug.Log("Entering Inventory Game State");
            _inventoryView.gameObject.SetActive(true);
        }

        protected override void OnExitState()
        {
            _inventoryView.gameObject.SetActive(false);
            Debug.Log("Exiting Inventory Game State");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnFixedUpdate()
        {
        }
    }
}
