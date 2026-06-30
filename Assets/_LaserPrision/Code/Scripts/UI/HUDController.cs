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
        [SerializeField] private PlayerInvulnerability playerInvulnerability;

        [SerializeField] private TMP_Text shieldStatusText;

        [SerializeField] private TMP_Text livesText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text timerText;

        private void Start()
        {
            playerHealth.LivesChanged += OnLivesChanged;
            scoreManager.ScoreChanged += OnScoreChanged;
            playerInvulnerability.InvulnerabilityStarted += OnShieldStarted;
            playerInvulnerability.CooldownFinished += OnShieldEnded;

            OnLivesChanged(playerHealth.CurrentLives);
            OnScoreChanged(scoreManager.CurrentScore,scoreManager.ElapsedTime);
        }

        private void OnLivesChanged(int lives)
        {
            livesText.text = $"Lives: {lives}";
        }

        private void OnScoreChanged(int score, float elapsedTime)
        {
            scoreText.text = $"Score: {score}";

            UpdateTimer(elapsedTime);
        }

        private void UpdateTimer(float time)
        {
            timerText.text = time.ToString();
        }

        private void OnShieldStarted()
        {
            shieldStatusText.text = "Shield: Deactive";
            shieldStatusText.color = Color.gray;
        }

        private void OnShieldEnded()
        {
            shieldStatusText.text = "Shield: Active";
            shieldStatusText.color = Color.blue;
        }
    }
}