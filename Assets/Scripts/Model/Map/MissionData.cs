using System;
using Containers;
using Model.Heroes;
using UnityEngine;

namespace Model.Map
{
    [Serializable]
    public class MissionData
    {
        public Vector2 positionOnScreen;
        public string id;
        public string[] requiredIdsToActivate;
        public string[] lockOnActiveUntilComplete;
        public string[] lockAfterCompleted;
        public MissionState state;

        public HeroIdIntContainer[] heroesPointsChange;
        public HeroId[] heroesUnlockOnComplete;
        public int selectedHeroPointsChange;

        public string missionName;
        public string textBeforeMission;
        public string textInMission;
        public string allies;
        public string enemies;
    }
}