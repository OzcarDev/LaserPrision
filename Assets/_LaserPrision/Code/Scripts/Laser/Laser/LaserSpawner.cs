using LaserPrison.Core;
using LaserPrison.Gameplay;
using System.Collections;
using UnityEngine;

namespace LaserPrison.Hazards
{
    public class LaserSpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private float spawnHeight = 10f;
        [SerializeField] private DifficultyLevel _currentSettings;

        [Header("Dependencies")]
        [SerializeField] private LaserPool laserPool;
        [SerializeField] private GameArea gameArea;
        [SerializeField] private Transform playerPosition;
        [SerializeField] private DifficultyManager difficultyManager;


        private Coroutine _spawnRoutine;

        

        private void Awake()
        {
            Debug.Assert(laserPool != null, "LaserPool reference is not assigned in LaserSpawner.");
            Debug.Assert(difficultyManager != null, "DifficultyManager reference is not assigned in LaserSpawner.");
            Debug.Assert(playerPosition != null, "Player Transform reference is not assigned in LaserSpawner.");
            Debug.Assert(gameArea != null, "GameArea Transform reference is not assigned in LaserSpawner.");
        }

        private void OnEnable()
        {
            if(difficultyManager != null) difficultyManager.DifficultyChanged += OnDifficultyChanged;
        }

        private void OnDisable()
        {
            if (GameManager.Instance != null) GameManager.Instance.GameStateChanged -= OnGameStateChanged;

            if (difficultyManager != null) difficultyManager.DifficultyChanged -= OnDifficultyChanged;
        }

        private void Start()
        {
            if (GameManager.Instance != null) GameManager.Instance.GameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            Debug.Log("OnGameStateChanged from LaserSpawner");
            if (state == GameState.Playing)
            {
                StartSpawning();
            }
            else
            {
                StopSpawning();
            }
        }

        private void StartSpawning()
        {
            StopSpawning();

            _spawnRoutine = StartCoroutine(SpawnLoop());
        }

        private void StopSpawning()
        {
            if (_spawnRoutine != null) StopCoroutine(_spawnRoutine);
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(_currentSettings.spawnInterval);
                SpawnWave();
            }
        }

        private void SpawnWave()
        {
            SpawnRandomLasers(_currentSettings.lasersPerWave);

            SpawnTargetedLasers(_currentSettings.targetedLasers);
        }

        private void SpawnRandomLasers(int quantity)
        {
            for(int index = 0; index < quantity; index++)
            {
                Vector3 position = gameArea.GetRandomPosition();

                SpawnLaser(position);
            }
        }

        private void SpawnTargetedLasers(int quantity)
        {
            for(int index = 0; index < quantity; index++)
            {
                Vector3 position = playerPosition.position;

                SpawnLaser(position);
            }
        }

        private void SpawnLaser(Vector3 position)
        {
            position.y = spawnHeight;

            Laser laser = laserPool.Get();

            laser.Activate(position, Quaternion.identity);
        }
        private void OnDifficultyChanged(DifficultyLevel difficulty)
        {
            _currentSettings = difficulty;

            if (GameManager.Instance.CurrentState == GameState.Playing) StartSpawning();
        }
    }
}