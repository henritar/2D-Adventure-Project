using Assets.Scripts.Runtime.Shared.Interfaces.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Runtime.Shared
{
    public abstract class BaseStatesManager<TStateEnum> : IStatesManager<TStateEnum>
        where TStateEnum : Enum
    {
        protected bool _isInitialized = false;
        public TStateEnum CurrentState { get; private set; }

        public bool IsInitialized => _isInitialized;

        protected IBaseState<TStateEnum>[] _states;
        protected IBaseState<TStateEnum> stateHandler;

        public BaseStatesManager(IEnumerable<IBaseState<TStateEnum>> states)
        {
            _states = states.ToArray();
            foreach (var state in _states)
            {
                state.SetStateManager(this);
            }

            _isInitialized = true;
        }

        public void ChangeState(TStateEnum state)
        {
            if (CurrentState.Equals(state) || !_isInitialized)
            {
                // Already in state or not initialized, no change needed
                return;
            }

            var oldState = CurrentState;
            var newState = _states.FirstOrDefault(s => s.State.Equals(state)) ?? throw new ArgumentException($"State {state} is not defined in the state manager.", nameof(state));

            CurrentState = state;

            if (stateHandler != null)
            {
                stateHandler.ExitState();
                stateHandler = default;
            }

            ChangeStateCallback(oldState, state);

            stateHandler = newState;

            stateHandler.EnterState();
        }

        public void Update()
        {
            stateHandler?.Update();
        }

        public void FixedUpdate()
        {
            stateHandler?.FixedUpdate();
        }

        public virtual void Dispose()
        {
        }

        protected virtual void ChangeStateCallback(TStateEnum oldState, TStateEnum newState) { }
    }
}
