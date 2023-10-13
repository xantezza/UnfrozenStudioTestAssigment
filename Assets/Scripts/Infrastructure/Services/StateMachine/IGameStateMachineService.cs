using Infrastructure.Abstractions;

namespace Infrastructure.Services.StateMachine
{
    public interface IGameStateMachineService : IService
    {
        public void Enter<TState>() where TState : class, IState
        {
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
        }
    }
}