using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Gameplay.Inventory.MVP;
using Assets.Scripts.Runtime.Gameplay.Player;
using Assets.Scripts.Runtime.Manager.States.MainGame;
using Assets.Scripts.Runtime.Shared.Interfaces.StateMachine;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerCharacterController _characterController;
        [SerializeField]
        private PlayerCharacterInputManager _characterInputManager;
        [SerializeField]
        private InventoryView _inventoryView;
        private GameStatesManager _gameStatesManager;

        void Awake()
        {
            List<IBaseState<GameStatesEnum>> gameStates = new List<IBaseState<GameStatesEnum>>();
            NoneGameState noneGameState = new NoneGameState();
            InventoryGameState inventoryGameState = new InventoryGameState(_characterInputManager, _inventoryView);
            PlayingGameState playingGameState = new PlayingGameState(_characterInputManager, _characterController);

            gameStates.Add(noneGameState);
            gameStates.Add(playingGameState);
            gameStates.Add(inventoryGameState);

            _gameStatesManager = new GameStatesManager(gameStates);
            noneGameState.SetStateManager(_gameStatesManager);
            playingGameState.SetStateManager(_gameStatesManager);
            inventoryGameState.SetStateManager(_gameStatesManager);

            DontDestroyOnLoad(this);
        }

        void Start()
        {
            _gameStatesManager.ChangeState(GameStatesEnum.Playing);
        }

        void Update()
        {
            _gameStatesManager.Update();
        }
    }
}
