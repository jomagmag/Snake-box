using UnityEngine;

namespace Snake_box
{
    public class TurretBonus:BaseBonus
    {
        private float _percentageA;
        private float _percentageB;
        
        public TurretBonus(TurretBonusData BonusData) : base(BonusData)
        {
            _percentageA = BonusData.PercentageA;
            _percentageB = BonusData.PercentageB;

        }


        public void Use(int a,int b)
        {
            if (Random.Range(0, 101) > _percentageA * a - _percentageB * (b - 1))
            {
                
            }else if (Random.Range(0, 101) > _percentageA * 2 * b)
            {
                
            }
            else
            {
                
            }
            
            base.Use();
        }
    }
}
