using Configs;
using Infrastructure.Services.PersistentProgress;
using Model.Heroes;
using UnityEngine;

namespace View.HeroChoose
{
    public class HeroChooseView : MonoBehaviour
    {
        private IPersistentProgressService _persistentProgressService;

        [SerializeField] private HeroButton[] _heroButtons;

        private HeroButton _currentSelectedHeroButton;
        private Config _config;

        private void OnEnable()
        {
            foreach (var heroButton in _heroButtons)
            {
                heroButton.OnHeroSelected += OnHeroSelected;
            }
        }

        private void OnDisable()
        {
            foreach (var heroButton in _heroButtons)
            {
                heroButton.OnHeroSelected -= OnHeroSelected;
            }
        }

        public void Init(Config config, IPersistentProgressService progressService)
        {
            _persistentProgressService = progressService;
            _config = config;

            foreach (var heroButton in _heroButtons)
            {
                heroButton.Init(config.HeroConfig, progressService);
            }
        }

        public void SetDefaultHero()
        {
            OnHeroSelected(_heroButtons[0], _config.HeroConfig.DefaultHero);
        }

        private void OnHeroSelected(HeroButton button, HeroId heroId)
        {
            if (_currentSelectedHeroButton != null) _currentSelectedHeroButton.SetSelection(false);
            _currentSelectedHeroButton = button;
            _currentSelectedHeroButton.SetSelection(true);
            _persistentProgressService.Progress.HeroesProgress.SelectHero(heroId);
        }
    }
}