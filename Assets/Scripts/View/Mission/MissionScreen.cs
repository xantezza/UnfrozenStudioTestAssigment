using System.Linq;
using Configs;
using Infrastructure;
using Infrastructure.Services.ConfigProvider;
using Infrastructure.Services.PersistentProgress;
using Model.Heroes;
using Model.Map;
using Model.PersistentProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View.Map;

namespace View.Mission
{
    public class MissionScreen : MonoBehaviour
    {
        [SerializeField] private MissionBlank _missionBlank;
        [SerializeField] private MapView _mapView;
        [SerializeField] private TextMeshProUGUI _missionNameLabel;
        [SerializeField] private TextMeshProUGUI _missionTextLabel;
        [SerializeField] private TextMeshProUGUI _alliesTextLabel;
        [SerializeField] private TextMeshProUGUI _enemiesTextLabel;
        [SerializeField] private Button _missionCompleteButton;
        [SerializeField] private GameObject _viewRoot;

        private IPersistentProgressService _progressService;
        private Config _config;

        private MissionData _currentMissionData;

        private HeroConfig HeroConfig => _config.HeroConfig;
        private HeroesProgress HeroProgress => _progressService.Progress.HeroesProgress;

        private void Start()
        {
            _progressService = ServiceProvider.Get<IPersistentProgressService>();
            _config = ServiceProvider.Get<IConfigProviderService>().Config;

            SetViewRootActive(false);
        }

        private void OnEnable()
        {
            _missionBlank.OnMissionStart += UpdateMissionView;
            _missionCompleteButton.onClick.AddListener(OnMissionCompleteClick);
        }

        private void OnDisable()
        {
            _missionBlank.OnMissionStart -= UpdateMissionView;
            _missionCompleteButton.onClick.RemoveListener(OnMissionCompleteClick);
        }

        private void SetViewRootActive(bool isActive)
        {
            _viewRoot.SetActive(isActive);
        }

        private void UpdateMissionView(MissionData missionData)
        {
            _currentMissionData = missionData;

            _missionNameLabel.text = $"{missionData.id}: {missionData.missionName}".Replace(
                HeroConfig.AntagonistPlaceholder,
                HeroProgress.GetAntagonist()
            );
            _missionTextLabel.text = missionData.textInMission.Replace(
                HeroConfig.AntagonistPlaceholder,
                HeroProgress.GetAntagonist()
            );

            _alliesTextLabel.text = missionData.allies.Replace(
                HeroConfig.AllyPlaceholder,
                HeroProgress.GetAlly()
            );
            _enemiesTextLabel.text = missionData.enemies.Replace(
                HeroConfig.EnemyPlaceholder,
                HeroProgress.GetEnemy()
            );

            SetViewRootActive(true);
        }

        private void OnMissionCompleteClick()
        {
            SetViewRootActive(false);
            _progressService.Progress.MissionsProgress.CompleteMission(_currentMissionData.id);
            _mapView.UpdateStates();
        }
    }
}