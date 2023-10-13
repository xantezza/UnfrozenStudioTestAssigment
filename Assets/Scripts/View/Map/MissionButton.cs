using System;
using Model.Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.Map
{
    [RequireComponent(typeof(RectTransform))]
    public class MissionButton : MonoBehaviour
    {
        public event Action<MissionButton> OnClick;

        [SerializeField] private Color defaultColor;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color completedColor;
        [SerializeField] private TextMeshProUGUI _missionID;
        [SerializeField] private Button _missionButton;

        private MissionData _missionData;

        public MissionData MissionData => _missionData;

        private void OnEnable()
        {
            _missionButton.onClick.AddListener(CallOnClickEvent);
        }

        private void OnDisable()
        {
            _missionButton.onClick.RemoveListener(CallOnClickEvent);
        }

        public void Init(MissionData data)
        {
            _missionData = data;

            _missionID.text = data.id;
            GetComponent<RectTransform>().anchoredPosition = data.positionOnScreen;

            UpdateState();
        }

        public void UpdateState()
        {
            var state = _missionData.state;

            gameObject.SetActive(state != MissionState.LockedAndHidden);
            _missionButton.interactable = state == MissionState.Active;
            if (state == MissionState.Completed) _missionButton.targetGraphic.color = completedColor;
        }

        public void SetSelection(bool isSelected)
        {
            _missionButton.targetGraphic.color = isSelected ? selectedColor : defaultColor;
        }

        private void CallOnClickEvent()
        {
            OnClick?.Invoke(this);
        }
    }
}