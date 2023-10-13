using Containers;
using Infrastructure.Abstractions;
using Infrastructure.Services.StateMachine;

namespace Infrastructure.GameStateMachineStates
{
    public class LoadLevelState : IPayloadedState<SceneNameCallbackForceReloadContainer>
    {
        private readonly GameStateMachineService _gameStateMachineService;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachineService gameStateMachineService, SceneLoader sceneLoader)
        {
            _gameStateMachineService = gameStateMachineService;
            _sceneLoader = sceneLoader;
        }

        public void Enter(SceneNameCallbackForceReloadContainer payLoad)
        {
            _sceneLoader.Load(payLoad.SceneName, payLoad.Callback, payLoad.ForceReload);
        }

        public void Exit() { }
    }
}