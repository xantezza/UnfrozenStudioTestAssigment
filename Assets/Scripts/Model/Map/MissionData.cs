using System;
using Model.Heroes;
using UnityEngine;

namespace Model.Map
{
    [Serializable]
    public struct MissionData
    {
        public Vector2 positionOnScreen;
        public string id;
        public string[] requiredIdsToActivate;
        public string[] lockOnActiveUntilComplete;
        public string[] lockAfterCompleted;
        public MissionState state;

        public HeroIdIntContainer[] heroesPointsChange;

        public string missionName;
        public string textBeforeMission;
        public string textInMission;
        public string allies;
        public string enemies;
    }
}