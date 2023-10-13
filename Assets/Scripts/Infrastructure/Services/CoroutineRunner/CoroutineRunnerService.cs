using System.Collections;
using UnityEngine;

namespace Infrastructure.Services.CoroutineRunner
{
    public class CoroutineRunnerService : MonoBehaviour, ICoroutineRunnerService
    {
        void ICoroutineRunnerService.RunCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}