using System;
using System.Linq;
using Configs;
using Infrastructure.Services.PersistentProgress;
using Model.Heroes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.HeroChoose
{
    public class HeroButton : MonoBehaviour
    {
        public event Action<HeroButton, HeroId> OnHeroSelected;

        [SerializeField] private HeroId _heroID;
        [SerializeField] private TextMeshProUGUI _heroPoints;
        [SerializeField] private TextMeshProUGUI _heroName;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _lockedRoot;

        private IPersistentProgressService _progressService;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnHeroButtonPressed);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnHeroButtonPressed);
        }

        public void Init(HeroConfig HeroConfig, IPersistentProgressService progressService)
        {
            _progressService = progressService;

            _heroName.text = HeroConfig.Names.First(x => x.HeroID == _heroID).String;
            
            UpdateLockedState();

            _progressService.Progress.HeroesProgress.OnHeroUnlocked += (e) => UpdateLockedState();
            _progressService.Progress.HeroesProgress.OnHeroPointsUpdate += UpdatePoints;
        }

        public void SetSelection(bool isSelected)
        {
            _button.targetGraphic.color = isSelected ? selectedColor : defaultColor;
        }

        private void UpdateLockedState()
        {
            _lockedRoot.SetActive(!_progressService.Progress.HeroesProgress.GetHeroUnlockState(_heroID));
        }
        
        private void UpdatePoints(HeroId heroId, int points)
        {
            if (heroId != _heroID) return;

            _heroPoints.text = points.ToString();
        }

        private void OnHeroButtonPressed()
        {
            OnHeroSelected?.Invoke(this, _heroID);
        }
    }
}