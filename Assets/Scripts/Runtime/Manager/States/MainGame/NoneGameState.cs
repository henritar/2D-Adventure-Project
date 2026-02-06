using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Shared;
using UnityEngine;

namespace Assets.Scripts.Runtime.Manager.States.MainGame
{
    public class NoneGameState : BaseGameState
    {
        protected override GameStatesEnum GameState => GameStatesEnum.None;

        protected override void OnEnterState()
        {
            Debug.Log("Entering None Game State");
        }

        protected override void OnExitState()
        {
            Debug.Log("Exiting None Game State");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnFixedUpdate()
        {
        }
    }
}
