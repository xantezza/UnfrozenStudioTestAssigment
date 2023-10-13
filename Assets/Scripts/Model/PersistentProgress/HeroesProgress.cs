using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using Containers;
using Model.Heroes;
using Model.Map;
using Unity.VisualScripting;

namespace Model.PersistentProgress
{
    public class HeroesProgress
    {
        public Action<HeroId, int> OnHeroPointsUpdate;
        public Action<HeroId> OnHeroUnlocked;

        private readonly Dictionary<HeroId, int> heroesPoints;
        private readonly Dictionary<HeroId, bool> heroesUnlockState;

        private HeroId _currentlySelectedHero;
        private readonly Config _config;

        public HeroesProgress(Config config, MissionsProgress missionsProgress)
        {
            _config = config;
            heroesPoints = new Dictionary<HeroId, int>();
            heroesUnlockState = new Dictionary<HeroId, bool>();

            foreach (var heroIdStringContainer in config.HeroConfig.Names)
            {
                heroesPoints.Add(heroIdStringContainer.HeroID, 0);
                heroesUnlockState.Add(heroIdStringContainer.HeroID, false);
            }

            heroesUnlockState[config.HeroConfig.DefaultHero] = true;

            missionsProgress.OnMissionComplete += OnMissionComplete;
        }

        public bool GetHeroUnlockState(HeroId heroId)
        {
            return heroesUnlockState[heroId];
        }

        public void SelectHero(HeroId heroId)
        {
            _currentlySelectedHero = heroId;
        }

        public string GetAlly()
        {
            return GetAlly(_config.HeroConfig.Allies);
        }

        public string GetEnemy()
        {
            return GetEnemy(_config.HeroConfig.Allies);
        }

        public string GetAntagonist()
        {
            return GetEnemy(_config.HeroConfig.Names);
        }

        private string GetAlly(HeroIdStringContainer[] containers)
        {
            var heroIdStringContainer = containers.FirstOrDefault(x => GetHeroUnlockState(x.HeroID));
            return heroIdStringContainer == null ? string.Empty : heroIdStringContainer.String;
        }

        private string GetEnemy(HeroIdStringContainer[] containers)
        {
            var heroIdStringContainer = containers.FirstOrDefault(x => !GetHeroUnlockState(x.HeroID));
            return heroIdStringContainer == null ? string.Empty : heroIdStringContainer.String;
        }

        private void OnMissionComplete(MissionData missionData)
        {
            ModifyHeroesPoints(missionData);
            TryUnlockHero(missionData);
        }

        private void ModifyHeroesPoints(MissionData missionData)
        {
            heroesPoints[_currentlySelectedHero] += missionData.selectedHeroPointsChange;
            OnHeroPointsUpdate?.Invoke(_currentlySelectedHero, heroesPoints[_currentlySelectedHero]);

            foreach (var heroIdIntContainer in missionData.heroesPointsChange)
            {
                heroesPoints[heroIdIntContainer.HeroID] += heroIdIntContainer.Int;
                OnHeroPointsUpdate?.Invoke(heroIdIntContainer.HeroID, heroesPoints[heroIdIntContainer.HeroID]);
            }
        }

        private void TryUnlockHero(MissionData missionData)
        {
            foreach (var newHeroId in missionData.heroesUnlockOnComplete)
            {
                heroesUnlockState[newHeroId] = true;
                OnHeroUnlocked?.Invoke(newHeroId);
            }
        }
    }
}