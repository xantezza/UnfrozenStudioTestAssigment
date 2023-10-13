using Model.Map;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "Configs/MapConfig")]
    public class MapConfig : ScriptableObject
    {
        [SerializeField] private MissionData[] _missions;

        public MissionData[] Missions => _missions;
    }
}