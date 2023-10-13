using System;
using System.Collections.Generic;
using Configs;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using Object = UnityEngine.Object;

namespace View.Map
{
    [Serializable]
    public class MapViewFactory
    {
        [SerializeField] private MissionButton _missionButtonPrefab;

        public List<MissionButton> InitializeMissionButtons(
            Config config, 
            IPersistentProgressService progressService, 
            Transform ButtonsParent, 
            Action<MissionButton> onMissionClickCallback
            )
        {
            var buttonsList = new List<MissionButton>();

            foreach (var missionData in config.MapConfig.Missions)
            {
                var missionButton = Object.Instantiate(
                    _missionButtonPrefab,
                    ButtonsParent
                );

                missionButton.Init(missionData, progressService, onMissionClickCallback);

                buttonsList.Add(missionButton);
            }

            return buttonsList;
        }
    }
}