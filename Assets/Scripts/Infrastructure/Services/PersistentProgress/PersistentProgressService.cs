using Configs;

namespace Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public Model.PersistentProgress.PersistentProgress Progress { get; }

        private Config _config;

        public PersistentProgressService(Config config)
        {
            _config = config;
            Progress = LoadProgressOrCreateNew();
        }

        //Загрузка и сохранение не реализована так как такая задача не была поставлена
        private Model.PersistentProgress.PersistentProgress LoadProgressOrCreateNew()
        {
            return new Model.PersistentProgress.PersistentProgress(_config);
        }
    }
}