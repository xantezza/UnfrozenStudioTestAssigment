using Configs;
using Containers;
using Infrastructure.Abstractions;
using Infrastructure.Services.CoroutineRunner;
using Infrastructure.Services.StateMachine;

namespace Infrastructure.GameStateMachineStates
{
    public class RegisterServicesState : IState
    {
        private readonly IGameStateMachineService _gameStateMachineService;
        private readonly Config _config;

        public RegisterServicesState(
            Config config,
            IGameStateMachineService gameStateMachineService,
            ICoroutineRunnerService coroutineRunnerService
        )
        {
            _gameStateMachineService = gameStateMachineService;
            _config = config;
            _coroutineRunnerService = coroutineRunnerService;
        }

        private readonly ICoroutineRunnerService _coroutineRunnerService;

        public void Enter()
        {
            ServiceProvider.RegisterServices(_config, _gameStateMachineService, _coroutineRunnerService);

            _gameStateMachineService.Enter<LoadLevelState, SceneNameCallbackForceReloadContainer>(
                new SceneNameCallbackForceReloadContainer(SceneNames.Main)
                );
        }

        public void Exit() { }
    }
}