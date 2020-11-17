using UnityEngine;

namespace Snake_box
{
    public sealed class BombBonus : BaseBonus
    {
        private readonly BombBonusData _bonusData;

        #region Fields

        private GameObject particle;
        private TimeRemaining _particleTimer;

        #endregion
        
        public BombBonus(BombBonusData BonusData) : base(BonusData)
        {
            _bonusData = BonusData;
        }
    }
}
