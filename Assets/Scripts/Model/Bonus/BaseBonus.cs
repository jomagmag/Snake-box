using UnityEngine;


namespace Snake_box
{
    public abstract class BaseBonus 
    {
        #region PrivateData

        protected GameObject _prefab;
        protected BonusType _type;

        #endregion

        #region Properties
        public GameObject _gameObject { get; private set; }
        public float CheckRadius { get; private set; }

        public BonusType Type => _type;
        

        #endregion

        #region Methods

        public BaseBonus(BonusData BonusData)
        {
            _prefab = BonusData.prefab;
            _type = BonusData.Type;
            CheckRadius = BonusData.Radius;
            Debug.Log(CheckRadius);
        }

        public virtual void Spawn(Transform transform)
        {
            _gameObject = GameObject.Instantiate(_prefab,transform.position,Quaternion.identity);
        }

        public virtual void Use()
        {
            Services.Instance.LevelService.ActiveBonus.Remove(this);
            Object.Destroy(_gameObject);
        }

        #endregion

    }
}