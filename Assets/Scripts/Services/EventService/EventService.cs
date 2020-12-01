using System;
using UnityEngine;

namespace Snake_box
{
    public class EventService : Service
    {
        #region Events

        public event Action LevelLoaded;
        public event Action LevelUnLoaded;
        public event Action WaveStarted;
        public event Action WaveEnded;
        public event Action TurretUpgraded;
        public event Action TurretAdded;
        public event Action TurretLevelUped;

        public event Action<Transform> SpawnedBonus;

        #endregion


        #region Fields

        #endregion


        #region ClassLifeCycle

        public EventService()
        {
            LevelLoaded += delegate { };
            LevelUnLoaded += delegate { };
            WaveStarted += delegate { };
            WaveEnded += delegate { };
            TurretAdded += delegate { };
            TurretUpgraded += delegate { };
            TurretLevelUped += delegate { };
            WaveEnded += WaveStart;
            TurretAdded += AddTurret;
        }

        #endregion


        #region Methods

        public void LevelLoad() => LevelLoaded?.Invoke();


        public void LevelUnload() => LevelUnLoaded?.Invoke();

        public void WaveStart() => WaveStarted?.Invoke();

        public void WaveEnd() => WaveEnded?.Invoke();

        public void TurretAdd() => TurretAdded?.Invoke();

        public void TurretUpgrade() => TurretUpgraded?.Invoke();

        public void TurretLevelUp() => TurretLevelUped?.Invoke();

        public void SpawnBonus(Transform transform) => SpawnedBonus?.Invoke(transform);

        public void AddTurret()
        {
            var point = Services.Instance.LevelService.CharacterBehaviour.GetPoint();
            if (point != null)
            {
                Data.Instance.TurretData.AddNewWithParent(point);
            }  
        }

        #endregion
    }
}
