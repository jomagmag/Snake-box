using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Snake_box
{
    public class EnemySpawnControler : IExecute, IInitialization
    {
        private List<Vector3> _spawnPoints = new List<Vector3>();
        private List<Vector3> _activeSpawnPoints = new List<Vector3>();
        private Queue<SingleEnemySpawnData> _enemiesToSpawnQueue;
        private int _currentEnemyPoints;
        private int _currentSpawnPoints;
        private int _bonusEnemyPoints;
        private int _currentSpawnCount;
        private TimeRemaining _nextWave;
        private TimeRemaining _nextSubWave;
        private LevelService _levelService;
        private WaveSettingsData _waveSettingsData;
        private Wave _currentWave;
        private Dictionary<int, int> _currentWavePointsWeight = new Dictionary<int, int>();
        private Dictionary<EnemyType, int> _EnemyWeight = new Dictionary<EnemyType, int>();
        private int _subWaveToSpawnCount;
        private int _subWaveSpawned;
        private List<EnemySettings> _enemySettings = new List<EnemySettings>();
        private List<EnemySettings> _activeEnemyList = new List<EnemySettings>();
        private int _enemySumWeight;
        private List<EnemySpawnWeight> _enemySpawnWeights = new List<EnemySpawnWeight>();


        /// <summary>
        /// Возвращает истинну, если все враги из списка были заспауненны
        /// </summary>
        private bool isSpawningFinished => _enemiesToSpawnQueue.Count == 0;

        #region ClassLifeCycle

        public EnemySpawnControler()
        {
            Services.Instance.LevelLoadService.LevelLoaded += GetAllSpawnPoints;
            Services.Instance.EventService.WaveStarted += GetWaveData;
            Services.Instance.EventService.WaveStarted += FillWaveWeight;
            Services.Instance.EventService.WaveEnded += EnemyPointsInc;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            /*
                        while (!isSpawningFinished && _enemiesToSpawnQueue.Peek().SpawnTiming <=
                               Services.Instance.TimeService.TimeSinceLevelStart())
                        {
                            SpawnNextEnemy();
                        }
            
                        if (isSpawningFinished)
                            Services.Instance.LevelService.IsLevelSpawnEnded = true;
            */
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            /*
            //Инициализация списка спауна
            //Получаем Scriptable Object список спауна
            var enemySpawnList = Data.Instance.LevelData.GetEnemySpawnList(Services.Instance.LevelService.CurrentLevel);
            //Извлекаем из него массив элементов - записей о спауне отдельных врагов
            var singleEnemySpawnDatas = enemySpawnList.Enemies;
            //Сортируем его по таймингу спауна по ворастанию
            singleEnemySpawnDatas = singleEnemySpawnDatas.OrderBy(x => x.SpawnTiming).ToArray();
            //Закидываем это всё в очередь в таком порядке, чтобы запси с ранними таймингами были в голове очереди
            _enemiesToSpawnQueue = new Queue<SingleEnemySpawnData>();
            foreach (var enemySpawnData in singleEnemySpawnDatas)
                _enemiesToSpawnQueue.Enqueue(enemySpawnData);

            //Инициализация точек спауна
            //Создаются массивы точек спауна и их ID соответственно
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Spawn));
            int[] spawnPointIds = new int[spawnPoints.Length];
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                string idString = string.Empty;//строка, содержащая все цифры из имени объекта
                foreach(char c in spawnPoints[i].name)
                {
                    if (IsDigit(c))
                        idString += c;
                }
                int id = int.Parse(idString);
                spawnPointIds[i] = id;
            }
            //Каждая точка спауна занимает место в массиве _spawnPoints с индексом равным своему ID
            _spawnPoints = new Vector3[spawnPointIds.Max() + 1];
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                _spawnPoints[spawnPointIds[i]] = spawnPoints[i].transform.position;
            }*/
            _levelService = Services.Instance.LevelService;
            _waveSettingsData = Data.Instance.WaveSettingsData;
            _currentEnemyPoints = _waveSettingsData.BasicEnemyPoints;
            _bonusEnemyPoints = _waveSettingsData.BonusEnemyPoints;
            _enemySettings = Data.Instance.EnemySpawnSettingsData.EnemySettings;
        }

        #endregion


        #region Methods

        private void GetAllSpawnPoints()
        {
            foreach (var points in GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Spawn)))
            {
                _spawnPoints.Add(points.transform.position);
            }
        }

        private void GetWaveData()
        {
            if (_waveSettingsData.Waves.Count > _levelService.CurrentWave)
            {
                _currentWave = _waveSettingsData.Waves[_levelService.CurrentWave];
                _currentWavePointsWeight.Clear();
                _currentSpawnCount = 0;
                _subWaveToSpawnCount = _currentWave.SuvWaves.Count;
                GetActivePoints(_currentWave.ActiveSpawnPoints);
                FillEnemyWeight();
                SpawnNextWave();
            }
        }

        private void FillEnemyWeight()
        {
            if (_enemySettings.Count < 1)
                foreach (var enemy in _enemySettings)
                {
                    _EnemyWeight.Add(enemy.EnemyType, enemy.EnemyWeight);
                }
        }

        private void FillWaveWeight()
        {
            int count = 0;
            _currentWavePointsWeight.Clear();
            _currentWavePointsWeight.Add(count, _currentWave.WaveWeight);
            if (_currentWave.SuvWaves != null)
                for (count = 1; count < _currentWave.SuvWaves.Count + 1; count++)
                {
                    _currentWavePointsWeight.Add(count, _currentWave.SuvWaves[count - 1].SubWaveWeight);
                }
        }

        private void CalcCurrentPoints()
        {
            int tempPoints = _currentEnemyPoints;
            _currentSpawnPoints = 0;
            int weightSum = _currentWavePointsWeight.Values.Sum();
            if (weightSum > 0)
                while (tempPoints > weightSum)
                {
                    for (int i = 0; i < _currentWavePointsWeight.Count; i++)
                    {
                        if (i == _currentSpawnCount)
                        {
                            _currentSpawnPoints += _currentWavePointsWeight[i];
                        }

                        tempPoints -= _currentWavePointsWeight[i];
                    }
                }
        }


        private void GetActivePoints(int activeSpawnPoints)
        {
            List<Vector3> tempPoints = _spawnPoints.ToList();

            _activeSpawnPoints.Clear();
            for (int i = 0; i < activeSpawnPoints; i++)
            {
                int rnd = Random.Range(0, tempPoints.Count);
                _activeSpawnPoints.Add(tempPoints[rnd]);
                tempPoints.RemoveAt(rnd);
            }
        }

        private void SpawnNextWave()
        {
            CalcCurrentPoints();
            SpawnWave();
            _subWaveSpawned = 0;
            _currentSpawnCount++;
            if (_subWaveToSpawnCount > 0)
            {
                _nextSubWave = new TimeRemaining(SpawnSubWave, _currentWave.SuvWaves[_subWaveSpawned].SubWaveTiming);
                _nextSubWave.AddTimeRemaining();
            }

            _nextWave = new TimeRemaining(Services.Instance.EventService.WaveEnd, _currentWave.WaveTiming);
            _nextWave.AddTimeRemaining();
        }


        private void SpawnWave()
        {
            GetActiveEnemy();
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            for (int i = 0; i < _activeSpawnPoints.Count; i++)
            {
                int spawnPoints = _currentEnemyPoints / _activeSpawnPoints.Count;
                var enemyToSpawn = RndType(_enemySpawnWeights, _enemySumWeight);
                int enemyPrice = 0;
                for (int j = 0; j < _enemySettings.Count; j++)
                {
                    if (_enemySettings[j].EnemyType == enemyToSpawn)
                    {
                        enemyPrice = _enemySettings[j].EnemySpawnCost;
                        break;
                    }
                }

                if (enemyPrice > 0)
                    while (spawnPoints >= enemyPrice)
                    {
                        spawnPoints -= enemyPrice;
                        SpawnNextEnemy(enemyToSpawn, _activeSpawnPoints[i]);
                    }
            }
        }

        private void GetActiveEnemy()
        {
            _activeEnemyList = _enemySettings.Where(enemy => enemy.EnemyMinWave <= _levelService.CurrentWave).ToList();
            _enemySumWeight = 0;
            _enemySpawnWeights.Add(new EnemySpawnWeight()
            {
                _type = _activeEnemyList[0].EnemyType,
                MinWeightToSpawn = 0,
                MaxWeightToSpawn = _activeEnemyList[0].EnemyWeight
            });
            _enemySumWeight += _activeEnemyList[0].EnemyWeight;
            for (int i = 1; i < _activeEnemyList.Count; i++)
            {
                _enemySpawnWeights.Add(new EnemySpawnWeight()
                {
                    _type = _activeEnemyList[i].EnemyType,
                    MinWeightToSpawn = _enemySpawnWeights[i - 1].MaxWeightToSpawn + 1,
                    MaxWeightToSpawn = _activeEnemyList[i].EnemyWeight + _enemySpawnWeights[i - 1].MaxWeightToSpawn + 1
                });
                _enemySumWeight += _activeEnemyList[i].EnemyWeight;
            }
        }


        private EnemyType RndType(List<EnemySpawnWeight> enemySpawnWeights, int maxWeight)
        {
            EnemyType enemyType = EnemyType.None;

            int rnd = Random.Range(0, maxWeight);

            for (int i = 0; i < enemySpawnWeights.Count; i++)
            {
                if (rnd >= enemySpawnWeights[i].MinWeightToSpawn && rnd < enemySpawnWeights[i].MaxWeightToSpawn)
                {
                    enemyType = enemySpawnWeights[i]._type;
                    break;
                }
            }

            return enemyType;
        }


        private void SpawnSubWave()
        {
            GetActivePoints(_currentWave.SuvWaves[_subWaveSpawned].SubWaveActiveSpawnPoints);
            CalcCurrentPoints();
            _subWaveToSpawnCount--;
            _subWaveSpawned++;
            SpawnWave();
            if (_subWaveToSpawnCount > 0)
            {
                _nextSubWave = new TimeRemaining(SpawnSubWave, _currentWave.SuvWaves[_subWaveSpawned].SubWaveTiming);
                _nextSubWave.AddTimeRemaining();
            }
        }


        private void EnemyPointsInc() => _currentEnemyPoints += _bonusEnemyPoints;

        private void SpawnNextEnemy(EnemyType type, Vector3 position)
        {
            BaseEnemy enemy = null;
            switch (type)
            {
                case EnemyType.Simple:
                    enemy = new SimpleEnemy();
                    break;
                case EnemyType.Fast:
                    enemy = new FastEnemy();
                    break;
                case EnemyType.Slow:
                    enemy = new SlowEnemy();
                    break;
                case EnemyType.Flying:
                    enemy = new FlyingEnemy();
                    break;
                case EnemyType.Accelerating:
                    enemy = new AcceleratingEnemy();
                    break;
                case EnemyType.Invisible:
                    enemy = new InvisibleEnemy();
                    break;
                case EnemyType.Spawned:
                    enemy = new SpawnedEnemy();
                    break;
                case EnemyType.Spawning:
                    enemy = new SpawningEnemy();
                    break;
                case EnemyType.Spiked:
                    enemy = new SpikedEnemy();
                    break;
            }

            enemy.Spawn(position);
        }

        private bool IsDigit(char character)
        {
            int parseResult;
            return int.TryParse(character.ToString(), out parseResult);
        }

        #endregion


        #region Struct

        private struct EnemySpawnWeight
        {
            public EnemyType _type;
            public int MinWeightToSpawn;
            public int MaxWeightToSpawn;
        }

        #endregion
    }
}
