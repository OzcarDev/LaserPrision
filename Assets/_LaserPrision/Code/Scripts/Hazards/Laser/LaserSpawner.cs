using LaserPrison.Core;
using System.Collections;
using UnityEngine;

namespace LaserPrison.Hazards
{
    public class LaserSpawner : MonoBehaviour
    {
        [SerializeField] private Laser laserPrefab;

        [SerializeField] private float spawnInterval = 2.5f;
        [SerializeField] private Vector2 areaSize = new Vector2(3f, 3f);
        [SerializeField] private float spawnHeight = 10f;

        private Coroutine _spawnRoutine;

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
            Vector3 randomPos = new Vector3(
                Random.Range(-areaSize.x, areaSize.x),
                spawnHeight,
                Random.Range(-areaSize.y, areaSize.y)
            );

            Instantiate(laserPrefab, randomPos, Quaternion.identity);
        }
    }
}