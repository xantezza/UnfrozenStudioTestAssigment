using Containers;
using Model.Heroes;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "Configs/HeroConfig")]
    public class HeroConfig : ScriptableObject
    {
        [Header("HeroId => Name")]
        [SerializeField] private HeroIdStringContainer[] _heroIdNamesContainers;
        
        [Header("HeroId => Ally/Enemy")]
        [SerializeField] private HeroIdStringContainer[] _heroIdAlliesContainers;

        [Space]
        [SerializeField] private HeroId _defaultHero;
        [SerializeField] private string _allyPlaceholder;
        [SerializeField] private string _enemyPlaceholder;
        [SerializeField] private string _antagonistPlaceholder;
        
        public HeroIdStringContainer[] Names => _heroIdNamesContainers;
        public HeroIdStringContainer[] Allies => _heroIdAlliesContainers;
        public HeroId DefaultHero => _defaultHero;
        
        public string AllyPlaceholder => _allyPlaceholder;
        public string EnemyPlaceholder => _enemyPlaceholder;
        public string AntagonistPlaceholder => _antagonistPlaceholder;
    }
}