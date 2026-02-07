using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Manager.States.MainGame;
using Assets.Scripts.Runtime.Shared.Interfaces.StateMachine;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _characterTransform;
        private GameStatesManager _gameStatesManager;

        void Awake()
        {
            List<IBaseState<GameStatesEnum>> gameStates = new List<IBaseState<GameStatesEnum>>();
            NoneGameState noneGameState = new NoneGameState();
            PlayingGameState playingGameState = new PlayingGameState(_characterTransform);

            gameStates.Add(noneGameState);
            gameStates.Add(playingGameState);

            _gameStatesManager = new GameStatesManager(gameStates);
            noneGameState.SetStateManager(_gameStatesManager);
            playingGameState.SetStateManager(_gameStatesManager);
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
