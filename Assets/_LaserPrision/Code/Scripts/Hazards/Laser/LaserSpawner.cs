using LaserPrison.Core;
using LaserPrison.Gameplay;
using System.Collections;
using UnityEngine;

namespace LaserPrison.Hazards
{
    public class LaserSpawner : MonoBehaviour
    {        
        [SerializeField] private DifficultyLevel _currentDifficulty;
        [SerializeField] private float spawnHeight = 10f;
        [SerializeField] private LaserPool laserPool;
        [SerializeField] private GameArea gameArea;
        [SerializeField] private Transform player;
        [SerializeField] private DifficultyManager difficultyManager;


        private Coroutine _spawnRoutine;

        

        private void Awake()
        {
            Debug.Assert(laserPool != null, "LaserPool reference is missing.");
        }
        private void Start()
        {
            GameManager.Instance.GameStateChanged += OnGameStateChanged;

            difficultyManager.DifficultyChanged += OnDifficultyChanged;
        }


        private void OnDestroy()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.GameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            if (state == GameState.Playing)
                StartSpawning();
            else
                StopSpawning();
        }

        private void StartSpawning()
        {
            if (_spawnRoutine != null)
                StopCoroutine(_spawnRoutine);

            _spawnRoutine = StartCoroutine(SpawnLoop());
        }

        private void StopSpawning()
        {
            if (_spawnRoutine != null)
                StopCoroutine(_spawnRoutine);
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(_currentDifficulty.spawnInterval);
                SpawnWave();
            }
        }

        private void SpawnWave()
        {
            SpawnRandomLasers(_currentDifficulty.lasersPerWave);

            SpawnTargetedLasers(_currentDifficulty.targetedLasers);
        }

        private void SpawnRandomLasers(int quantity)
        {
            for(int index = 0; index < quantity; index++)
            {
                SpawnLaser(gameArea.GetRandomPosition(spawnHeight));
            }
        }

        private void SpawnTargetedLasers(int quantity)
        {
            for(int index = 0; index < quantity; index++)
            {
                Vector3 position = player.position;
                position.y = spawnHeight;

                SpawnLaser(position);
            }
        }

        private void SpawnLaser(Vector3 position)
        {
            Laser laser = laserPool.Get();

            laser.Activate(position, Quaternion.identity);
        }
        private void OnDifficultyChanged(DifficultyLevel difficulty)
        {
            _currentDifficulty = difficulty;
        }
    }
}