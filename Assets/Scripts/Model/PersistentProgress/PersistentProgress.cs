using Configs;

namespace Model.PersistentProgress
{
    public class PersistentProgress
    {
        public readonly HeroesProgress HeroesProgress;
        public readonly MissionsProgress MissionsProgress;

        public PersistentProgress(Config config)
        {
            MissionsProgress = new MissionsProgress(config);
            HeroesProgress = new HeroesProgress(config, MissionsProgress);
        }
    }
}