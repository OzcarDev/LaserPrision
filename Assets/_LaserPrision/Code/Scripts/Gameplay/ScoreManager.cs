using LaserPrison.Core;
using System;
using System.Collections;
using UnityEngine;

namespace LaserPrison.Gameplay
{
    public class ScoreManager : MonoBehaviour
    {
        public int CurrentScore { get; private set; }
        public float ElapsedTime { get; private set; }

        public event Action<int> ScoreChanged;

        private Coroutine _scoreRoutine;

        private void Start()
        {
            GameManager.Instance.GameStateChanged += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null) GameManager.Instance.GameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Playing:
                    StartScoring();
                    break;

                case GameState.GameOver:
                    StopScoring();
                    break;
            }
        }

        private void StartScoring()
        {
            if (_scoreRoutine != null) StopCoroutine(_scoreRoutine);

            _scoreRoutine = StartCoroutine(ScoreLoop());
        }

        private void StopScoring()
        {
            if (_scoreRoutine != null)
                StopCoroutine(_scoreRoutine);
        }

        private IEnumerator ScoreLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                ElapsedTime += 1f;

                int multiplier = GetMultiplier(ElapsedTime);

                CurrentScore += multiplier;

                ScoreChanged?.Invoke(CurrentScore);
            }
        }

        private int GetMultiplier(float time)
        {
            if (time < 15f) return 1;
            if (time < 40f) return 2;
            if (time < 90f) return 3;
            return 5;
        }
    }
}