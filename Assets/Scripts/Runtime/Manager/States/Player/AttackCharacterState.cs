using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Gameplay.Player;
using Assets.Scripts.Runtime.Shared;
using UnityEngine;

namespace Assets.Scripts.Runtime.Manager.States.Player
{
    public class AttackCharacterState : BaseState<CharacterStateEnum>
    {
        protected override CharacterStateEnum CurrentState => CharacterStateEnum.Attack;

        private PlayerCharacterController _characterController;

        public AttackCharacterState(PlayerCharacterController characterController)
        {
            _characterController = characterController;
        }

        protected override void OnEnterState()
        {
            Debug.Log("Entering Attack Character State");
        }

        protected override void OnExitState()
        {
            Debug.Log("Exiting Attack Character State");
        }

        protected override void OnUpdate()
        {
            _characterController.OnAttack();
        }

        protected override void OnFixedUpdate()
        {
        }
    }
}
