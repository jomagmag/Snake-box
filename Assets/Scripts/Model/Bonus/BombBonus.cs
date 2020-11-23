using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Snake_box
{
    public sealed class BombBonus : BaseBonus
    {
        #region Fields

        private GameObject particle;
        private GameObject particleObject;
        private TimeRemaining _particleTimer;
        private float _effectTimer;
        private float _radius;
        private float _damage;
        private LevelService _levelService = Services.Instance.LevelService;
        private bool usePartricle;

        #endregion
        
        public BombBonus(BombBonusData BonusData) : base(BonusData)
        {
            particle = BonusData.particle;
            _effectTimer = BonusData.particleTimer;
            _damage = BonusData.explosionDamage;
            _radius = BonusData.explosionRadius;
            usePartricle = BonusData.UseParticle;
            _particleTimer = new TimeRemaining(ParticleDestroy,_effectTimer);
        }

        public override void Use()
        {

            int count = 0;
            
            for (int i = 0; i < _levelService.ActiveEnemies.Count; i++)
            {
                if (_levelService.ActiveEnemies[i].GetTransform().position
                        .CalcDistance(_gameObject.transform.position) < _radius)
                {
                    count++;
                    var enemy = _levelService.ActiveEnemies[i] as BaseEnemy;
                    if (enemy != null) enemy.RegisterDamage(_damage, ArmorTypes.None);
                }
            }

            if (usePartricle)
            {
                Services.Instance.LevelService.ActiveBonus.Remove(this);
                particleObject = GameObject.Instantiate(particle,_gameObject.transform.position,Quaternion.identity);
                _particleTimer.AddTimeRemaining();
            }
            else
                base.Use();
        }

        public void ParticleDestroy()
        {
            Object.Destroy(particleObject);
            Object.Destroy(_gameObject);
        }
    }
}
