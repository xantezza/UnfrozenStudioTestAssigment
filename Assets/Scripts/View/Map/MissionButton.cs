using System;
using Infrastructure.Services.PersistentProgress;
using Model.Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.Map
{
    [RequireComponent(typeof(RectTransform))]
    public class MissionButton : MonoBehaviour
    {
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color completedColor;
        [SerializeField] private TextMeshProUGUI _missionID;
        [SerializeField] private Button _missionButton;

        private MissionData _missionData;
        private IPersistentProgressService _progressService;
        private Action<MissionButton> _cachedOnClickCallback;
        private bool isCompleted = false;
        public MissionData MissionData => _missionData;

        private void OnEnable()
        {
            _missionButton.onClick.AddListener(CallOnClickEvent);
        }

        private void OnDisable()
        {
            _missionButton.onClick.RemoveListener(CallOnClickEvent);
        }

        public void Init(MissionData data, IPersistentProgressService progressService, Action<MissionButton> onMissionClickCallback)
        {
            _missionData = data;
            _cachedOnClickCallback = onMissionClickCallback;
            _progressService = progressService;
            isCompleted = false;

            _missionID.text = data.id;
            GetComponent<RectTransform>().anchoredPosition = data.positionOnScreen;

            UpdateState();
        }

        public void UpdateState()
        {
            var state = _progressService.Progress.MissionsProgress.GetStateByID(_missionData.id);

            gameObject.SetActive(state != MissionState.LockedAndHidden);
            _missionButton.interactable = state == MissionState.Active;
            
            if (state == MissionState.Completed)
            {
                _missionButton.targetGraphic.color = completedColor;
                isCompleted = true;
            }
        }

        public void SetSelection(bool isSelected)
        {
            if (isCompleted) return;

            _missionButton.targetGraphic.color = isSelected ? selectedColor : defaultColor;
        }

        private void CallOnClickEvent()
        {
            _cachedOnClickCallback?.Invoke(this);
        }
    }
}