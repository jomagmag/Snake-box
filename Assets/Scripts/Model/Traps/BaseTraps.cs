
using UnityEngine;

namespace Snake_box
{
    public abstract class BaseTraps : IExecute, ITrap
    {
        protected GameObject _gameObject;
        protected bool _isActive;
        private float rechargeTime;
        private float rechargeDelay;


        public BaseTraps(BaseTrapsData data)
        {
            rechargeDelay = data.ReloadTime;
            _gameObject = GameObject.Instantiate(data.Prefab, data.SpawnPos);
        }
        
        #region IExecute

        public void Execute()
        {
            if (_isActive)
            {
                Active();
            }
            else
            {
                if (rechargeTime < 0f)
                {
                    _isActive = true;
                    rechargeTime = rechargeDelay;

                }
                else
                {
                    rechargeTime -= Time.deltaTime;
                }
            }
        }

        #endregion

        #region ITrap

        public abstract void Active();


        #endregion
    }
}
