using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.ConfigProvider;
using Infrastructure.Services.PersistentProgress;
using Model.Map;
using UnityEngine;

namespace View.Map
{
    public class MapView : MonoBehaviour
    {
        public event Action<MissionData> OnMissionDataSelected;

        [SerializeField] private MapViewFactory _mapViewFactory;

        private List<MissionButton> _missionButtons;
        private MissionButton _currentSelectedMission;
        private bool isInitialized = false;

        private void Start()
        {
            Initialize(
                ServiceProvider.Get<IConfigProviderService>().Config,
                ServiceProvider.Get<IPersistentProgressService>()
            );
        }

        public void UpdateStates()
        {
            foreach (var missionButton in _missionButtons)
            {
                missionButton.UpdateState();
            }
        }

        private void Initialize(Config config, IPersistentProgressService progressService)
        {
            if (isInitialized) return;
            _missionButtons = _mapViewFactory.InitializeMissionButtons(config, progressService, transform, SelectMissionButton);
            isInitialized = true;
        }

        private void SelectMissionButton(MissionButton button)
        {
            if (_currentSelectedMission != null) _currentSelectedMission.SetSelection(false);
            _currentSelectedMission = button;
            _currentSelectedMission.SetSelection(true);

            OnMissionDataSelected?.Invoke(button.MissionData);
        }
    }
}