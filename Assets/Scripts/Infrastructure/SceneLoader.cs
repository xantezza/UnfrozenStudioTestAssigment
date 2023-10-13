using System;
using System.Collections;
using Infrastructure.Services.CoroutineRunner;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunnerService _coroutineRunnerService;

        public SceneLoader(ICoroutineRunnerService coroutineRunnerService)
        {
            _coroutineRunnerService = coroutineRunnerService;
        }

        public void Load(string sceneName, Action onLoaded = null, bool forceReload = false)
        {
            _coroutineRunnerService.RunCoroutine(LoadScene(sceneName, onLoaded, forceReload));
        }

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null, bool forceReload = false)
        {
            if (SceneManager.GetActiveScene().name == nextScene && !forceReload)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
            {
                yield return null;
            }

            onLoaded?.Invoke();
        }
    }
}