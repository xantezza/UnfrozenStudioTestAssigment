using System;
using System.Collections.Generic;
using Configs;
using Infrastructure.Abstractions;
using Infrastructure.GameStateMachineStates;
using Infrastructure.Services.CoroutineRunner;

namespace Infrastructure.Services.StateMachine
{
    public class GameStateMachineService : IGameStateMachineService
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachineService(Config config, SceneLoader sceneLoader, ICoroutineRunnerService coroutineRunnerService)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(RegisterServicesState)] = new RegisterServicesState(config, this, coroutineRunnerService),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
                [typeof(GameLoopState)] = new GameLoopState()
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state?.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state?.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}