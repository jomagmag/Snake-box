
using UnityEngine;

namespace Snake_box
{
    public abstract class BaseTraps : IExecute, ITrap
    {
        protected GameObject _gameObject;
        protected bool _isActive;
        private float rechargeTime;
        private float rechargeDelay;
        private BaseTrapsData _data;


        public BaseTraps(BaseTrapsData data)
        {
            _data = data;
            rechargeDelay = data.ReloadTime;
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

        public void Spawn()
        {
            _gameObject = GameObject.Instantiate(_data.Prefab);
            _gameObject.transform.position = GameObject.FindGameObjectWithTag(TagManager.GetTag(TagType.GrenadeLauncherPos)).transform.position;
        }

        public void Destroy()
        {
            Object.Destroy(_gameObject);
        }


        #endregion
    }
}
