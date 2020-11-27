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
        private int _subWaveCount;
        private List<EnemySettings> _enemySettings = new List<EnemySettings>();


        /// <summary>
        /// Возвращает истинну, если все враги из списка были заспауненны
        /// </summary>
        private bool isSpawningFinished => _enemiesToSpawnQueue.Count == 0;

        #region ClassLifeCycle

        public EnemySpawnControler()
        {
            Services.Instance.LevelLoadService.LevelLoaded += GetAllSpawnPoints;
            Services.Instance.EventService.WaveStarted += GetWaveData;
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
            FillWaveWeight();
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
                Debug.Log(_levelService.CurrentWave);
                _currentWave = _waveSettingsData.Waves[_levelService.CurrentWave];
                _currentWavePointsWeight.Clear();
                _currentSpawnCount = 0;
                GetActivePoints();
                FillEnemyWeight();
                Debug.Log("WaveWeight = " + _currentWave.WaveWeight);
                SpawnNextWave();
            }
        }

        private void FillEnemyWeight()
        {
            foreach (var enemy in _enemySettings)
            {
                _EnemyWeight.Add(enemy.EnemyType, enemy.EnemyWeight);
            }
        }

        private void FillWaveWeight()
        {
            int count = 0;
            _currentWavePointsWeight.Add(count, _currentWave.WaveWeight);
            foreach (var VARIABLE in _currentWavePointsWeight)
            {
                Debug.Log("key = "+VARIABLE.Key+" Valuse = " + VARIABLE.Value);
            }
            if (_currentWave.SuvWaves != null)
                for (count = 1; count < _currentWave.SuvWaves.Count + 1; count++)
                {
                    _currentWavePointsWeight.Add(count, _currentWave.SuvWaves[count - 1].SubWaveWeight);
                }
        }

        private void CalcCurrentPoints()
        {
            int weightSum = 0;
            int tempPoints = _currentEnemyPoints;
            _currentSpawnPoints = 0;
            foreach (var weight in _currentWavePointsWeight.Values)
            {
                weightSum += weight;
            }
            Debug.Log(weightSum);
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

            Debug.Log("exitWhile");
        }


        private void GetActivePoints()
        {
            List<Vector3> tempPoints = _spawnPoints.ToList();

            _activeSpawnPoints.Clear();
            for (int i = 0; i < _currentWave.ActiveSpawnPoints; i++)
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
        }


        private void SpawnWave()
        {
            var activeEnemyList =
                _enemySettings.Where(enemy => enemy.EnemyMinWave <= _levelService.CurrentWave).ToList();
            var enemySumWeight = 0;
            List<EnemySpawnWeinght> enemySpawnWeinghts = new List<EnemySpawnWeinght>();
            enemySpawnWeinghts.Add(new EnemySpawnWeinght()
            {
                _type = activeEnemyList[0].EnemyType,
                MinWeightToSpawn = 0,
                MaxWeightToSpawn = activeEnemyList[0].EnemyWeight
            });
            enemySumWeight += activeEnemyList[0].EnemyWeight;
            for (int i = 1; i < activeEnemyList.Count; i++)
            {
                enemySpawnWeinghts.Add(new EnemySpawnWeinght()
                {
                    _type = activeEnemyList[i].EnemyType,
                    MinWeightToSpawn = enemySpawnWeinghts[i - 1].MaxWeightToSpawn + 1,
                    MaxWeightToSpawn = activeEnemyList[i].EnemyWeight + enemySpawnWeinghts[i - 1].MaxWeightToSpawn + 1
                });
                enemySumWeight += activeEnemyList[i].EnemyWeight;
            }

            for (int i = 0; i < _activeSpawnPoints.Count; i++)
            {
                int spawnPoints = _currentEnemyPoints / _activeSpawnPoints.Count;
                var enemyToSpawn = RndType(enemySpawnWeinghts, enemySumWeight);
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
                    while (spawnPoints > enemyPrice)
                    {
                        spawnPoints -= enemyPrice;
                        SpawnNextEnemy(enemyToSpawn, _activeSpawnPoints[i]);
                    }
            }
        }


        private EnemyType RndType(List<EnemySpawnWeinght> enemySpawnWeinghts, int maxWeight)
        {
            EnemyType enemyType = EnemyType.None;

            int rnd = Random.Range(0, maxWeight);

            for (int i = 0; i < enemySpawnWeinghts.Count; i++)
            {
                if (rnd >= enemySpawnWeinghts[i].MinWeightToSpawn && rnd < enemySpawnWeinghts[i].MaxWeightToSpawn)
                {
                    enemyType = enemySpawnWeinghts[i]._type;
                    break;
                }
            }

            return enemyType;
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

        private struct EnemySpawnWeinght
        {
            public EnemyType _type;
            public int MinWeightToSpawn;
            public int MaxWeightToSpawn;
        }

        #endregion
    }
}
