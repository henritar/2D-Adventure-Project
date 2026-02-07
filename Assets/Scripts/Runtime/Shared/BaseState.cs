using Assets.Scripts.Runtime.Shared.Interfaces.StateMachine;
using System;

namespace Assets.Scripts.Runtime.Shared
{
    public abstract class BaseState<TState> : IBaseState<TState>, IDisposable where TState : Enum
    {
        protected IStatesManager<TState> _stateManager;
        public TState State => CurrentState;

        public void SetStateManager(IStatesManager<TState> stateManager)
        {
            _stateManager = stateManager;
        }

        public void EnterState()
        {
            OnEnterState();
        }

        public void ExitState()
        {
            OnExitState();
        }

        public void FixedUpdate()
        {
            OnFixedUpdate();
        }
        public void Update()
        {
            OnUpdate();
        }
        public void Dispose()
        {
            OnDisposing();
            _stateManager = null;
        }

        protected abstract TState CurrentState { get; }
        protected abstract void OnEnterState();
        protected abstract void OnExitState();
        protected abstract void OnFixedUpdate();
        protected abstract void OnUpdate();
        protected virtual void OnDisposing() { }
    }
}
