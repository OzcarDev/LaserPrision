using LaserPrison.Core;
using LaserPrison.Gameplay;
using System.Collections;
using UnityEngine;

namespace LaserPrison.Hazards
{
    public class LaserSpawner : MonoBehaviour
    {

        [SerializeField] private float spawnInterval = 2.5f;
        [SerializeField] private float spawnHeight = 10f;
        [SerializeField] private LaserPool laserPool;
        [SerializeField] private GameArea gameArea;
        private Coroutine _spawnRoutine;

        private void Awake()
        {
            Debug.Assert(laserPool != null, "LaserPool reference is missing.");
        }
        private void Start()
        {
            GameManager.Instance.GameStateChanged += OnGameStateChanged;
            OnGameStateChanged(GameState.Playing);
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
                SpawnLaser();
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void SpawnLaser()
        {
            Laser laser = laserPool.Get();

            laser.Activate(gameArea.GetRandomPosition(spawnHeight),Quaternion.identity);
        }
    }
}