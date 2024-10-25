using System;
using System.Collections.Generic;
using Infarastructure.GameStateMachine.States;
using Infrastucture.StateMachine;
using Zenject;

namespace Infarastructure.GameStateMachine
{
    public class GameStateMachine : IInitializable
    {
        private Dictionary<Type, IExitableState> _states;
        private StateResolverService _stateResolverService;

        private IExitableState _currentState;

        public GameStateMachine(StateResolverService stateResolverService)
        {
            _stateResolverService = stateResolverService;
            
        }
        
        public void Initialize()
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = _stateResolverService.GetState<BootstrapState>(),
                [typeof(WaitingPlayersState)] = _stateResolverService.GetState<WaitingPlayersState>(),
                [typeof(MenuState)] = _stateResolverService.GetState<MenuState>(),
                [typeof(GameLoopState)] = _stateResolverService.GetState<GameLoopState>(),
            };
            
            Enter<BootstrapState>();
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TPayloadedState, TPayload>(TPayload payload) where TPayloadedState : class, IPayloadedState<TPayload>
        {
            ChangeState<TPayloadedState>().Enter(payload);
        }
        
        public TState GetState<TState>() where TState : class, IExitableState
        {
            TState state = _states[typeof(TState)] as TState;
            
            if (state == null)
                throw new ArgumentNullException(nameof(TState));

            return state;
        }

        public TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;
            return state;
        }
    }
}
