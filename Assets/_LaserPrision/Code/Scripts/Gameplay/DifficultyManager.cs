using UnityEngine;
using LaserPrison.Core;
using LaserPrison.Hazards;
using System;

namespace LaserPrison.Gameplay
{
    public class DifficultyManager : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreManager;
        [Space]
        [SerializeField] private DifficultySettings difficultySettings;

        private DifficultyLevel _currentLevel;

        public Action<DifficultyLevel> DifficultyChanged;

        private void Start()
        {
            EvaluateDifficulty();
        }

        private void Update()
        {
            EvaluateDifficulty();
        }

        private void EvaluateDifficulty()
        {
            float elapsedTime = scoreManager.ElapsedTime;

            DifficultyLevel level = GetCurrentDifficulty(elapsedTime);

            if (level == _currentLevel)
                return;

            _currentLevel = level;

            DifficultyChanged?.Invoke(_currentLevel);
        }

        private DifficultyLevel GetCurrentDifficulty(float elapsedTime)
        {
            DifficultyLevel result = difficultySettings.levels[0];

            foreach (DifficultyLevel level in difficultySettings.levels)
            {
                if (elapsedTime >= level.startTime)
                    result = level;
            }

            return result;
        }
    }
}