namespace Infrastructure.Abstractions
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}