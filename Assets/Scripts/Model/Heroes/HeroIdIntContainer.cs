using System;

namespace Model.Heroes
{
    [Serializable]
    public struct HeroIdIntContainer
    {
        public HeroId HeroID;
        public int ScoreChange;
    }
}