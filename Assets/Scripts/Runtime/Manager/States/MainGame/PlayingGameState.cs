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
        private PlayerCharacterController _characterController;

        public PlayingGameState(PlayerCharacterInputManager playerCharacterInputManager, PlayerCharacterController characterController)
        {
            _playerCharacterInputManager = playerCharacterInputManager;
            _characterController = characterController;

            List<IBaseState<CharacterStateEnum>> characterStates = new List<IBaseState<CharacterStateEnum>>();
            NoneCharacterState noneState = new NoneCharacterState();
            AttackCharacterState attackState = new AttackCharacterState(characterController);
            InteractCharacterState interactState = new InteractCharacterState(characterController);
            MoveCharacterState moveState = new MoveCharacterState(_playerCharacterInputManager, characterController);

            characterStates.Add(moveState);
            characterStates.Add(noneState);
            characterStates.Add(interactState);
            characterStates.Add(attackState);

            _characterStatesManager = new CharacterStatesManager(characterStates);
            moveState.SetStateManager(_characterStatesManager);
            attackState.SetStateManager(_characterStatesManager);
            interactState.SetStateManager(_characterStatesManager);
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
            _playerCharacterInputManager.ToggleInputManager(true);
            _playerCharacterInputManager.MoveInputAction += ChangeToFromMoveState;
            _playerCharacterInputManager.InteractPressedAction += ChangeToFromInteractState;
            _playerCharacterInputManager.AttackPressedAction += ChangeToFromAttackState;
            _playerCharacterInputManager.InventoryPressedAction += ChangeToInventoryState;
        }

        void OnDisable()
        {
            _playerCharacterInputManager.MoveInputAction -= ChangeToFromMoveState;
            _playerCharacterInputManager.InteractPressedAction -= ChangeToFromInteractState;
            _playerCharacterInputManager.AttackPressedAction -= ChangeToFromAttackState;
            _playerCharacterInputManager.InventoryPressedAction -= ChangeToInventoryState;
            _playerCharacterInputManager.ToggleInputManager(false);
        }

        void ChangeToFromMoveState(bool isMoving)
        {
            if (_characterController.IsInteracting)
                return;

            _characterStatesManager.ChangeState(isMoving ? CharacterStateEnum.Move : CharacterStateEnum.None);
        }

        void ChangeToFromInteractState(bool isInteracting)
        {
            _characterStatesManager.ChangeState(isInteracting ? CharacterStateEnum.Interact : CharacterStateEnum.None);
        }

        void ChangeToFromAttackState(bool isAttacking)
        {
            _characterStatesManager.ChangeState(isAttacking ? CharacterStateEnum.Attack : CharacterStateEnum.None);
        }

        void ChangeToInventoryState(bool isInventoryPressed)
        {
            if (isInventoryPressed)
            {
                _stateManager.ChangeState(GameStatesEnum.Inventory);
            }
        }
    }
}
