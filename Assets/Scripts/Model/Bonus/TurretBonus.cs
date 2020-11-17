using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Snake_box
{
    public sealed class TurretBonus : BaseBonus
    {
        #region PrivateData

        private float _percentageA;
        private float _percentageB;
        private EventService _eventService = Services.Instance.EventService;

        #endregion

        public TurretBonus(TurretBonusData BonusData) : base(BonusData)
        {
            _percentageA = BonusData.PercentageA;
            _percentageB = BonusData.PercentageB;
        }


        public void Use(int a, int b)
        {
            var rnd = Random.Range(0, 101);
            if (rnd < (_percentageA * a) - (_percentageB * (b - 1)))
                _eventService.TurretAdd();
            else if (rnd < _percentageA * 2 * b)
                _eventService.TurretUpgrade();
            else
                _eventService.TurretLevelUp();


            base.Use();
        }
    }
}
