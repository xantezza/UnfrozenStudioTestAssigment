namespace Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        public Model.PersistentProgress.PersistentProgress Progress { get; }
    }
}