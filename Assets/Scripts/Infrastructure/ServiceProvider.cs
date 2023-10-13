using System;
using System.Collections.Generic;
using Configs;
using Infrastructure.Services;
using Infrastructure.Services.ConfigProvider;
using Infrastructure.Services.CoroutineRunner;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.StateMachine;

namespace Infrastructure
{
    public static class ServiceProvider
    {
        private static Dictionary<Type, IService> _registeredServices;

        public static void RegisterServices(
            Config config,
            IGameStateMachineService gameStateMachineService,
            ICoroutineRunnerService coroutineRunnerService
        )
        {
            _registeredServices = new Dictionary<Type, IService>();

            _registeredServices.Add(typeof(ICoroutineRunnerService), coroutineRunnerService);
            _registeredServices.Add(typeof(IGameStateMachineService), gameStateMachineService);
            _registeredServices.Add(typeof(IConfigProviderService), new ConfigProviderService(config));
            _registeredServices.Add(typeof(IPersistentProgressService), new PersistentProgressService(config));
        }

        /// <summary>
        /// Call only from Start for consistency
        /// </summary>
        public static TService Get<TService>() where TService : class, IService
        {
            return _registeredServices[typeof(TService)] as TService;
        }
    }
}