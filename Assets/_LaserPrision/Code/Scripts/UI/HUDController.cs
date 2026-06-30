using TMPro;
using UnityEngine;
using LaserPrison.Player;
using LaserPrison.Gameplay;

namespace LaserPrison.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private ScoreManager scoreManager;

        [SerializeField] private TMP_Text livesText;
        [SerializeField] private TMP_Text scoreText;

        private void Start()
        {
            playerHealth.LivesChanged += OnLivesChanged;
            scoreManager.ScoreChanged += OnScoreChanged;

            OnLivesChanged(playerHealth.CurrentLives);
            OnScoreChanged(scoreManager.CurrentScore);
        }

        private void OnLivesChanged(int lives)
        {
            livesText.text = $"Lives: {lives}";
        }

        private void OnScoreChanged(int score)
        {
            scoreText.text = $"Score: {score}";
        }
    }
}