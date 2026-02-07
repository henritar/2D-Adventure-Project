using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Shared;
using UnityEngine;

namespace Assets.Scripts.Runtime.Manager.States.Character
{
    public class NoneCharacterState : BaseState<CharacterStateEnum>
    {
        protected override CharacterStateEnum CurrentState => CharacterStateEnum.None;

        protected override void OnEnterState()
        {
            Debug.Log("Entering None Character State");
        }

        protected override void OnExitState()
        {
            Debug.Log("Exiting None Character State");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnFixedUpdate()
        {
        }
    }
}
