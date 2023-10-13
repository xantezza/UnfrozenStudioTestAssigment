using Configs;
using Infrastructure;
using Infrastructure.Services.CoroutineRunner;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public static bool IsExist { get; private set; }

    [SerializeField] private Config _config;
    [SerializeField] private CoroutineRunnerService _coroutineRunnerService;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        IsExist = true;
        new Game(_config, _coroutineRunnerService);
    }
}