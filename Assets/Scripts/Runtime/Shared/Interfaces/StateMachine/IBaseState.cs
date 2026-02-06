using System;

namespace Assets.Scripts.Runtime.Shared.Interfaces.StateMachine
{
    public interface IBaseState<TStateEnum> where TStateEnum : Enum
    {
        TStateEnum State { get; }

        void SetStateManager(IStatesManager<TStateEnum> stateManager);
        void EnterState();
        void ExitState();
        void Update();
        void FixedUpdate();
    }
}
