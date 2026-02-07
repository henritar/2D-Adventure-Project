using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Manager.States.Character;
using Assets.Scripts.Runtime.Shared;
using Assets.Scripts.Runtime.Shared.Interfaces.StateMachine;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Manager.States.MainGame
{
    public class PlayingGameState : BaseState<GameStatesEnum>
    {
        private CharacterStatesManager _characterStatesManager;

        public PlayingGameState(Gameplay.Character.PlayerCharacterController characterController)
        {
            List<IBaseState<CharacterStateEnum>> characterStates = new List<IBaseState<CharacterStateEnum>>();
            NoneCharacterState noneState = new NoneCharacterState();
            ActionCharacterState actionState = new ActionCharacterState();

            actionState.CharacterController = characterController;
            characterStates.Add(actionState);
            characterStates.Add(noneState);

            _characterStatesManager = new CharacterStatesManager(characterStates);
            actionState.SetStateManager(_characterStatesManager);
            noneState.SetStateManager(_characterStatesManager);
        }

        protected override GameStatesEnum CurrentState => GameStatesEnum.Playing;

        protected override void OnEnterState()
        {
            Debug.Log("Entering Playing Game State");
            _characterStatesManager.ChangeState(CharacterStateEnum.Action);
            
        }

        protected override void OnExitState()
        {
            _characterStatesManager.ChangeState(CharacterStateEnum.None);
            Debug.Log("Exiting Playing Game State");
        }

        protected override void OnUpdate()
        {
            _characterStatesManager.Update();
        }

        protected override void OnFixedUpdate()
        {
            _characterStatesManager.FixedUpdate();
        }
    }
}
