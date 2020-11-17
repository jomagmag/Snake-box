using System;
using System.Linq;
using System.Resources;

namespace Snake_box
{
    public class RapidFireBonus: BaseBonus
    {
        #region Fields

        private float _rapidFireModifier;
        private float _duration;
        private TimeRemaining _timeRemaining;

        #endregion
     
        public RapidFireBonus(RapidFireBonusData BonusData) : base(BonusData)
        {
            _rapidFireModifier = BonusData.RapidFireMultiplier;
            _duration = BonusData.BonusDuration;
            _timeRemaining = new TimeRemaining(SetModifier,_duration);
        }



        public override void Use()
        {
            SetModifier(_rapidFireModifier);
            base.Use();
        }

        private void SetModifier(float mod) => Data.Instance.TurretData.GetTurretList()
            .ForEach(abs => abs.SetFireRate(_rapidFireModifier));
        private void SetModifier() => Data.Instance.TurretData.GetTurretList()
            .ForEach(abs => abs.SetFireRate(1));
    }
}
