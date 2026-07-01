using UnityEngine;
using UnityEngine.SceneManagement;
using LaserPrison.Core;
using LaserPrison.Gameplay;
using TMPro;

namespace LaserPrison.UI
{
    public class GameUI : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private GameObject GameMenuUI;
        [SerializeField] private GameObject GamePlayUI;
        [Header("UI References")]
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private TMP_Text finalScore;

        private void Start()
        {
            if (GameManager.Instance == null) return; GameManager.Instance.GameStateChanged += OnGameStateChanged;
            GameOverUI.SetActive(false);
            GameMenuUI.SetActive(true);
        }

        private void OnDisable()
        {
            if (GameManager.Instance == null) return; GameManager.Instance.GameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Waiting:
                    GameMenuUI.SetActive(true);
                    GamePlayUI.SetActive(false);
                    GameOverUI.SetActive(false);
                    break;

                case GameState.Playing:
                    GameMenuUI.SetActive(false);
                    GamePlayUI.SetActive(true);
                    GameOverUI.SetActive(false);
                    break;

                case GameState.GameOver:
                    GameOverUI.SetActive(true);
                    GamePlayUI.SetActive(false);
                    finalScore.text = $"Score:{scoreManager.CurrentScore}";
                    break;
                default:
                    GameMenuUI.SetActive(true);
                    break;
            }
        }
    }
}