using LaserPrison.Core;
using System;
using UnityEngine;

namespace LaserPrison.Gameplay
{
    public class DifficultyManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private ScoreManager scoreManager;

        [Header("Settings")]
        [SerializeField] private DifficultySettings difficultySettings;


        public event Action<DifficultyLevel> DifficultyChanged;

        private int _currentLevelIndex;

        private DifficultyLevel CurrentLevel => difficultySettings.Levels[_currentLevelIndex];

        private void Awake()
        {
            Debug.Assert(scoreManager != null, "ScoreManager is not assigned in DifficultyManager");
            Debug.Assert(difficultySettings != null, "DifficultySettings is not assigned in DifficultyManager");
            Debug.Assert(difficultySettings.Levels.Length > 0, "Difficulty Settings is empty");
        }

        private void OnEnable()
        {
            if (scoreManager != null) scoreManager.TimeChanged += EvaluateDifficulty;
        }

        private void OnDisable()
        {
            if (GameManager.Instance != null) GameManager.Instance.GameSessionReset -= ResetDifficulty;

            if (scoreManager != null) scoreManager.TimeChanged -= EvaluateDifficulty;
        }

        private void Start()
        {
            if (GameManager.Instance != null) GameManager.Instance.GameSessionReset += ResetDifficulty;

            ResetDifficulty();
        }

        private void EvaluateDifficulty(float elapsedTime)
        {
            if (IsLastLevel()) return;

            DifficultyLevel nextLevel = difficultySettings.Levels[_currentLevelIndex + 1];

            if(elapsedTime >= nextLevel.startTime)
            {
                _currentLevelIndex++;
                DifficultyChanged?.Invoke(CurrentLevel);
            }
        }

        private void ResetDifficulty()
        {
            if (_currentLevelIndex == 0) return;

            _currentLevelIndex = 0;
            DifficultyChanged?.Invoke(CurrentLevel);
        }

        private bool IsLastLevel()
        {
            return _currentLevelIndex >= difficultySettings.Levels.Length - 1;
        }
    }
}