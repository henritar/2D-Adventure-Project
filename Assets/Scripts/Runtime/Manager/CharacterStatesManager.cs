using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Shared;
using Assets.Scripts.Runtime.Shared.Interfaces.StateMachine;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Manager
{
    public class CharacterStatesManager : BaseStatesManager<CharacterStateEnum>
    {
        public CharacterStatesManager(IEnumerable<IBaseState<CharacterStateEnum>> gameStates) : base(gameStates)
        {
        }
    }
}
