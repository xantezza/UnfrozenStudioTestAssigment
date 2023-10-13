using System.Collections;

namespace Infrastructure.Services.CoroutineRunner
{
    public interface ICoroutineRunnerService : IService
    {
        void RunCoroutine(IEnumerator coroutine);
    }
}