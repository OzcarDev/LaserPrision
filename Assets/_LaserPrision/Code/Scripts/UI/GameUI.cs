using UnityEngine;
using UnityEngine.SceneManagement;
using LaserPrison.Core;

namespace LaserPrison.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private GameObject GameMenuUI;

        private void Start()
        {
            GameOverUI.SetActive(false);
            GameMenuUI.SetActive(true);
        }

        private void OnEnable()
        {
            if (GameManager.Instance == null) return;
            GameManager.Instance.GameStateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            if (GameManager.Instance == null) return;
            GameManager.Instance.GameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {

            switch (state)
            {
                case GameState.Waiting:
                    GameMenuUI.SetActive(true);
                    break;

                case GameState.Playing:
                    GameMenuUI.SetActive(false);
                    break;

                case GameState.GameOver:
                    GameOverUI.SetActive(true);
                    break;
                default:
                    GameMenuUI.SetActive(true);
                    break;
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}