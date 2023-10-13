using System;
using Model.Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View.Map;

namespace View
{
    public class MissionBlank : MonoBehaviour
    {
        public event Action<MissionData> OnMissionStart;

        [SerializeField] private MapView _mapView;

        [SerializeField] private TextMeshProUGUI _missionNameLabel;
        [SerializeField] private TextMeshProUGUI _textBeforeMissionLabel;
        [SerializeField] private Button _startMissionButton;

        private MissionData _currentSelectedMissionData;

        private void OnEnable()
        {
            _mapView.OnMissionDataSelected += UpdateMissionDataView;
            _startMissionButton.onClick.AddListener(OnStartMissionButtonClick);
        }

        private void OnDisable()
        {
            _mapView.OnMissionDataSelected -= UpdateMissionDataView;
            _startMissionButton.onClick.RemoveListener(OnStartMissionButtonClick);
        }

        private void UpdateMissionDataView(MissionData data)
        {
            _currentSelectedMissionData = data;
            _missionNameLabel.text = $"{data.id}: {data.missionName}";
            _textBeforeMissionLabel.text = data.textBeforeMission;
        }

        private void OnStartMissionButtonClick()
        {
            OnMissionStart?.Invoke(_currentSelectedMissionData);
        }
    }
}