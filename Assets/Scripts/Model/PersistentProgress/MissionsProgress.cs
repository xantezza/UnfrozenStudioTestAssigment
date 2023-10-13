using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using Model.Map;
using UnityEngine;

namespace Model.PersistentProgress
{
    public class MissionsProgress
    {
        public Action<MissionData> OnMissionComplete;

        private readonly Dictionary<string, MissionState> _missionStates;
        private readonly Config _config;

        public MissionsProgress(Config config)
        {
            _config = config;
            _missionStates = new Dictionary<string, MissionState>();

            foreach (var missionData in config.MapConfig.Missions)
            {
                if (!_missionStates.TryAdd(missionData.id, missionData.state))
                {
                    Debug.LogError("Can't add element to dictionary, duplicate id in config");
                }
            }
        }

        public void CompleteMission(string completedMissionId)
        {
            if (!_missionStates.ContainsKey(completedMissionId))
            {
                Debug.LogError($"Mission id {completedMissionId} not found! Wrong id!");
                return;
            }

            _missionStates[completedMissionId] = MissionState.Completed;

            foreach (var missionData in _config.MapConfig.Missions)
            {
                TryActivateMission(missionData);

                TryLockActiveMissionsAfterComplete(missionData);

                TryLockActiveMissionUntilComplete(missionData);
            }

            OnMissionComplete?.Invoke(_config.MapConfig.Missions.First(x => x.id == completedMissionId));
        }

        public MissionState GetStateByID(string id)
        {
            return _missionStates[id];
        }

        private void TryActivateMission(MissionData missionData)
        {
            if (_missionStates[missionData.id] != MissionState.LockedAndHidden) return;

            var mustBeLocked = false;

            foreach (var requiredID in missionData.requiredIdsToActivate)
            {
                if (requiredID.Contains("or") && !mustBeLocked)
                {
                    var requiredIDS = requiredID.Split(" or ");

                    var mustBeUnlocked = false;

                    foreach (var requiredIDFromSplit in requiredIDS)
                    {
                        if (_missionStates[requiredIDFromSplit] == MissionState.Completed)
                        {
                            mustBeUnlocked = true;
                        }
                    }

                    mustBeLocked = !mustBeUnlocked;
                }
                else if (_missionStates[requiredID] != MissionState.Completed)
                {
                    mustBeLocked = true;
                }
            }

            if (!mustBeLocked)
            {
                _missionStates[missionData.id] = MissionState.Active;
            }
        }

        private void TryLockActiveMissionsAfterComplete(MissionData missionData)
        {
            if (_missionStates[missionData.id] != MissionState.Completed) return;

            foreach (var idToLock in missionData.lockAfterCompleted)
            {
                _missionStates[idToLock] = MissionState.Locked;
            }
        }

        private void TryLockActiveMissionUntilComplete(MissionData missionData)
        {
            if (_missionStates[missionData.id] != MissionState.Active) return;

            foreach (var idToLock in missionData.lockOnActiveUntilComplete)
            {
                _missionStates[idToLock] = MissionState.Locked;
            }
        }
    }
}