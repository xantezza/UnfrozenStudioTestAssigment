using System;
using System.Collections.Generic;
using Configs;
using Model.Map;
using UnityEngine;

namespace View.Map
{
    public class MapView : MonoBehaviour
    {
        public event Action<MissionData> OnMissionDataSelected;

        [SerializeField] private MapConfig _mapConfig;
        [SerializeField] private MissionButton _missionButtonPrefab;

        private readonly List<MissionButton> _missionButtons = new List<MissionButton>();

        private MissionButton _currentSelectedMission;

        private void Awake()
        {
            InitializeMissions();
        }

        private void InitializeMissions()
        {
            foreach (var missionData in _mapConfig.Missions)
            {
                var missionButton = Instantiate(
                    _missionButtonPrefab,
                    transform
                );

                missionButton.Init(missionData);

                missionButton.OnClick += OnMissionButtonClicked;

                _missionButtons.Add(missionButton);
            }
        }

        private void OnMissionButtonClicked(MissionButton button)
        {
            if (_currentSelectedMission != null) _currentSelectedMission.SetSelection(false);
            _currentSelectedMission = button;
            _currentSelectedMission.SetSelection(true);
            
            OnMissionDataSelected?.Invoke(button.MissionData);
        }
    }
}