using Configs;
using Infrastructure.GameStateMachineStates;
using Infrastructure.Services.CoroutineRunner;
using Infrastructure.Services.StateMachine;

namespace Infrastructure
{
    public class Game
    {
        private IGameStateMachineService GameStateMachineService { get; }

        public Game(Config config, ICoroutineRunnerService coroutineRunnerService)
        {
            var sceneLoader = new SceneLoader(coroutineRunnerService);
            GameStateMachineService = new GameStateMachineService(config, sceneLoader, coroutineRunnerService);
            
            GameStateMachineService.Enter<RegisterServicesState>();
        }
    }
}