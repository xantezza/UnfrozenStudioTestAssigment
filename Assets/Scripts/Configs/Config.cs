using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Config", menuName = "Configs/Config")]
    public class Config : ScriptableObject
    {
        [SerializeField] private MapConfig _mapConfig;
        [SerializeField] private HeroConfig _heroConfig;

        public MapConfig MapConfig => _mapConfig;
        public HeroConfig HeroConfig => _heroConfig;
    }
}