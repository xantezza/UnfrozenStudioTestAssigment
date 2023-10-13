using Infrastructure;
using Infrastructure.GameStateMachineStates;
using Infrastructure.Services.ConfigProvider;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.StateMachine;
using UnityEngine;
using View.HeroChoose;

namespace View
{
    public class GameLoopEntryPoint : MonoBehaviour
    {
        [SerializeField] private HeroChooseView _heroChooseView;

        private void Start()
        {
            ServiceProvider.Get<IGameStateMachineService>().Enter<GameLoopState>();

            var config = ServiceProvider.Get<IConfigProviderService>().Config;
            var persistentProgress = ServiceProvider.Get<IPersistentProgressService>();

            _heroChooseView.Init(
                config,
                persistentProgress
            );
            _heroChooseView.SetDefaultHero();
        }
    }
}