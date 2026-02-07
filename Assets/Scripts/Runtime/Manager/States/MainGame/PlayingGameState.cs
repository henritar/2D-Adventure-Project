using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Gameplay.Player;
using Assets.Scripts.Runtime.Manager.States.Player;
using Assets.Scripts.Runtime.Shared;
using Assets.Scripts.Runtime.Shared.Interfaces.StateMachine;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Manager.States.MainGame
{
    public class PlayingGameState : BaseState<GameStatesEnum>
    {
        private PlayerCharacterInputManager _playerCharacterInputManager;
        private CharacterStatesManager _characterStatesManager;

        public PlayingGameState(PlayerCharacterInputManager playerCharacterInputManager, PlayerCharacterController characterController)
        {
            _playerCharacterInputManager = playerCharacterInputManager;

            List<IBaseState<CharacterStateEnum>> characterStates = new List<IBaseState<CharacterStateEnum>>();
            NoneCharacterState noneState = new NoneCharacterState();
            ActionCharacterState actionState = new ActionCharacterState(_playerCharacterInputManager, characterController);

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

            OnEnable();
            _characterStatesManager.ChangeState(CharacterStateEnum.None);
        }

        protected override void OnExitState()
        {
            _characterStatesManager.ChangeState(CharacterStateEnum.None);
            OnDisable();

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


        void OnEnable()
        {
            _playerCharacterInputManager.MoveInputAction += ChangeToFromMoveState;
            _playerCharacterInputManager.InteractPressedAction += ChangeToFromInteractState;
            _playerCharacterInputManager.AttackPressedAction += ChangeToFromAttackState;
        }

        void OnDisable()
        {
            _playerCharacterInputManager.MoveInputAction -= ChangeToFromMoveState;
            _playerCharacterInputManager.InteractPressedAction -= ChangeToFromInteractState;
            _playerCharacterInputManager.AttackPressedAction -= ChangeToFromAttackState;
        }

        void ChangeToFromMoveState(bool isMoving)
        {
            _characterStatesManager.ChangeState(isMoving ? CharacterStateEnum.Action : CharacterStateEnum.None);
        }

        void ChangeToFromInteractState(bool isInteracting)
        {
            _characterStatesManager.ChangeState(isInteracting ? CharacterStateEnum.Action : CharacterStateEnum.None);
        }

        void ChangeToFromAttackState(bool isAttacking)
        {
            _characterStatesManager.ChangeState(isAttacking ? CharacterStateEnum.Action : CharacterStateEnum.None);
        }
    }
}
