using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Snake_box
{
    public class BonusController : IInitialization, IExecute ///отвечает за место и время спауна
    {

        #region PrivateData

        private List<Transform> _spawnPoints= new List<Transform>();//точки споуна бонусов
        private TimeRemaining _spawnInvoker; //список всех бонусов
        private int _spawnTime= 25;
        private EventService Events = Services.Instance.EventService;
        private List<BaseBonus> _bonuslist = Services.Instance.LevelService.ActiveBonus;
        private Collider[] _colliders = new Collider[10];
        private CharacterBehaviour _characterBehaviour;

        #endregion

        #region Methods

        public void Initialization()
        {
            Events.WaveStarted += SpawnBonus;
            _spawnInvoker = new TimeRemaining(SpawnBonus,2);
            _spawnInvoker.AddTimeRemaining();
            _characterBehaviour = Services.Instance.LevelService.CharacterBehaviour;
        }        
        public void Execute()
        {
            CheckBonus();
        }

        private void CheckBonus()
        {
            for (int i = 0; i < _bonuslist.Count; i++)
            {
                Physics.OverlapSphereNonAlloc(_bonuslist[i]._gameObject.transform.position, _bonuslist[i].Radius, _colliders);
                for (int j = 0; j < _colliders.Length; j++)
                {
                    if (_colliders[i].CompareTag(TagManager.GetTag(TagType.Player)))
                    {
                        switch (_bonuslist[i].Type)
                        {
                            case BonusType.Turret:
                                var _bonus = _bonuslist[i] as TurretBonus;
                                _bonus.Use(_characterBehaviour.GetTurretPoints(),_characterBehaviour.GetActiveTurret());
                                break;
                            case BonusType.Bomb:
                                break;
                            case BonusType.RapidFire:
                                break;
                            case BonusType.Heal:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    break;
                }
            }
        }

        private void SpawnBonus()
        {
            _spawnPoints.Add(GameObject.FindGameObjectWithTag("BonusPoint").transform); //TODO Переделать под спаун на рандомной позиции
            BaseBonus bonus = new TurretBonus(Data.Instance.TurretBonusData);
            _bonuslist.Add(bonus);
            bonus.Spawn(_spawnPoints[0]);
        }

        #endregion

    }
}
