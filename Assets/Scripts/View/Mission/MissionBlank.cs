using System;
using Configs;
using Infrastructure;
using Infrastructure.Services.ConfigProvider;
using Infrastructure.Services.PersistentProgress;
using Model.Map;
using Model.PersistentProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View.Map;

namespace View.Mission
{
    public class MissionBlank : MonoBehaviour
    {
        public event Action<MissionData> OnMissionStart;

        [SerializeField] private MapView _mapView;

        [SerializeField] private TextMeshProUGUI _missionNameLabel;
        [SerializeField] private TextMeshProUGUI _textBeforeMissionLabel;
        [SerializeField] private Button _startMissionButton;
        [SerializeField] private GameObject _viewRoot;

        private IPersistentProgressService _progressService;
        private Config _config;

        private MissionData _currentSelectedMissionData;

        private HeroConfig HeroConfig => _config.HeroConfig;
        private HeroesProgress HeroProgress => _progressService.Progress.HeroesProgress;

        private void Start()
        {
            _config = ServiceProvider.Get<IConfigProviderService>().Config;
            _progressService = ServiceProvider.Get<IPersistentProgressService>();

            SetViewRootActive(false);
        }

        private void OnEnable()
        {
            _mapView.OnMissionDataSelected += UpdateMissionView;
            _startMissionButton.onClick.AddListener(OnStartMissionButtonClick);
        }

        private void OnDisable()
        {
            _mapView.OnMissionDataSelected -= UpdateMissionView;
            _startMissionButton.onClick.RemoveListener(OnStartMissionButtonClick);
        }

        private void SetViewRootActive(bool isActive)
        {
            _viewRoot.SetActive(isActive);
        }

        private void UpdateMissionView(MissionData missionData)
        {
            _currentSelectedMissionData = missionData;
            
            _missionNameLabel.text = $"{missionData.id}: {missionData.missionName}".Replace(
                HeroConfig.AntagonistPlaceholder,
                HeroProgress.GetAntagonist()
            );
            _textBeforeMissionLabel.text = missionData.textBeforeMission.Replace(
                HeroConfig.AntagonistPlaceholder,
                HeroProgress.GetAntagonist()
            );
            
            SetViewRootActive(true);
        }

        private void OnStartMissionButtonClick()
        {
            SetViewRootActive(false);
            OnMissionStart?.Invoke(_currentSelectedMissionData);
        }
    }
}