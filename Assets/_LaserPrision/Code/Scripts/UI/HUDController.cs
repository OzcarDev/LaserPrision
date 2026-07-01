using TMPro;
using UnityEngine;
using LaserPrison.Player;
using LaserPrison.Gameplay;

namespace LaserPrison.UI
{
    public class HUDController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private PlayerInvulnerability playerInvulnerability;

        [Header("UI References")]
        [SerializeField] private TMP_Text shieldStatusText;
        [SerializeField] private TMP_Text livesText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text timerText;


        private void Awake()
        {
            Debug.Assert(playerHealth != null, "Player Health is not assigned in HUDController");
            Debug.Assert(scoreManager != null, "Score Manager is not assigned in HUDController");
            Debug.Assert(playerInvulnerability != null, "Player Invulnerability is not assigned in HUDController");
        }

        private void OnEnable()
        {
            if(playerHealth != null)playerHealth.LivesChanged += OnLivesChanged;

            if(playerInvulnerability != null)
            {
                playerInvulnerability.InvulnerabilityStarted += OnShieldStarted;
                playerInvulnerability.CooldownFinished += OnShieldEnded;
            }

            if(scoreManager!= null) scoreManager.ScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            if (playerHealth != null) playerHealth.LivesChanged -= OnLivesChanged;

            if (playerInvulnerability != null)
            {
                playerInvulnerability.InvulnerabilityStarted -= OnShieldStarted;
                playerInvulnerability.CooldownFinished -= OnShieldEnded;
            }

            if (scoreManager != null) scoreManager.ScoreChanged -= OnScoreChanged;
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