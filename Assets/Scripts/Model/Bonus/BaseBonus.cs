using UnityEngine;


namespace Snake_box
{
    public abstract class BaseBonus 
    {
        #region PrivateData

        protected GameObject _prefab;
        protected BonusType _type;

        #endregion


        #region Methods

        public BaseBonus(BonusData BonusData)
        {
            _prefab = BonusData.prefab;
            _type = BonusData.Type;
        }

        public virtual void Spawn(Transform transform)
        {
            GameObject.Instantiate(_prefab,transform.position,Quaternion.identity);
            Debug.Log("Created");
        }

        public virtual void Use()
        {
            Object.Destroy(_prefab);
            Services.Instance.LevelService.ActiveBonus.Remove(this);
        }
        
        #endregion

    }
}