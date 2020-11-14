using System.Collections.Generic;
using UnityEngine;


namespace Snake_box
{
    public class BonusSpawnController : IInitialization ///отвечает за место и время спауна
    {

        #region PrivateData

        private List<Transform> _spawnPoints= new List<Transform>();//точки споуна бонусов
        private TimeRemaining _spawnInvoker; //список всех бонусов
        private int _spawnTime= 25;       

        #endregion

        #region Methods

        public void Initialization()
        {
            _spawnInvoker = new TimeRemaining(SpawnBonus,2);
            _spawnInvoker.AddTimeRemaining();
        }        

        private void SpawnBonus()
        {
            _spawnPoints.Add(GameObject.FindGameObjectWithTag("BonusPoint").transform);
            Debug.Log("SpawnBonus");
            BaseBonus bonus = new TurretBonus(Data.Instance.TurretBonusData);
            Services.Instance.LevelService.ActiveBonus.Add(bonus);
            bonus.Spawn(_spawnPoints[0]);
        }

        #endregion

    }
}
