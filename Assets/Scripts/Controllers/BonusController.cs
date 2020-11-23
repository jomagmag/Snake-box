using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Snake_box
{
    public class BonusController : IInitialization, IExecute ///отвечает за место и время спауна
    {
        #region PrivateData

        private List<Transform> _spawnPoints = new List<Transform>(); //точки споуна бонусов
        private TimeRemaining _spawnInvoker; //список всех бонусов
        private int _spawnTime = 25;
        private EventService Events = Services.Instance.EventService;
        private List<BaseBonus> _bonuslist = Services.Instance.LevelService.ActiveBonus;
        private CharacterBehaviour _characterBehaviour;

        #endregion

        #region Methods

        public void Initialization()
        {
            Events.WaveStarted += SpawnTurretBonus;
            Events.SpawnedBonus += SpawnBonus;
            _spawnInvoker = new TimeRemaining(SpawnTurretBonus, 2);
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
                Collider[] _colliders = new Collider[30];
                Physics.OverlapSphereNonAlloc(_bonuslist[i]._gameObject.transform.position, _bonuslist[i].CheckRadius,
                    _colliders);
                for (int j = 0; j < _colliders.Length; j++)
                {
                    if (_colliders[j] != null)
                        if (_colliders[j].CompareTag(TagManager.GetTag(TagType.Player)))
                        {
                            switch (_bonuslist[i].Type)
                            {
                                case BonusType.Turret:
                                {
                                    var bonus = _bonuslist[i] as TurretBonus;
                                    bonus.Use(_characterBehaviour.GetTurretPoints(),
                                        _characterBehaviour.GetActiveTurret());
                                }
                                    break;
                                case BonusType.Bomb:
                                {
                                    var bonus = _bonuslist[i] as BombBonus;
                                    bonus.Use();
                                }

                                    break;
                                case BonusType.RapidFire:
                                {
                                    var bonus = _bonuslist[i] as RapidFireBonus;
                                    bonus.Use();
                                }
                                    break;
                                case BonusType.Heal:
                                {
                                    var bonus = _bonuslist[i] as HealBonus;
                                    bonus.Use();
                                }
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }

                            break;
                        }
                }
            }
        }

        private void SpawnTurretBonus()
        {
            _spawnPoints.Add(GameObject.FindGameObjectWithTag("BonusPoint")
                .transform); //TODO Переделать под спаун на рандомной позиции
            BaseBonus bonus = new TurretBonus(Data.Instance.TurretBonusData);
            _bonuslist.Add(bonus);
            bonus.Spawn(_spawnPoints[0]);
        }

        private void SpawnBonus(Transform transform)
        {
            int rnd = Random.Range(2, 5);
            BaseBonus _bonus;
            switch (rnd)
            {
                case 2:
                    _bonus = new BombBonus(Data.Instance.BombBonusData);
                    _bonuslist.Add(_bonus);
                    _bonus.Spawn(transform);
                    break;
                case 3:
                    _bonus = new HealBonus(Data.Instance.HealBonusData);
                    _bonuslist.Add(_bonus);
                    _bonus.Spawn(transform);
                    break;
                case 4:
                    _bonus = new RapidFireBonus(Data.Instance.RapidFireBonusData);
                    _bonuslist.Add(_bonus);
                    _bonus.Spawn(transform);
                    break;
            }
        }

        #endregion
    }
}
