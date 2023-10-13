using System;
using Model.Heroes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class HeroButton : MonoBehaviour
    {
        public event Action<HeroId> OnHeroSelected;

        [SerializeField] private HeroId _heroID;
        [SerializeField] private TextMeshProUGUI _heroPoints;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnHeroButtonPressed);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnHeroButtonPressed);
        }

        private void OnHeroButtonPressed()
        {
            OnHeroSelected?.Invoke(_heroID);
        }
    }
}