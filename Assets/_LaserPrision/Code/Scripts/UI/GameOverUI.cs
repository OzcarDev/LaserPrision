using UnityEngine;
using UnityEngine.SceneManagement;
using LaserPrison.Core;

namespace LaserPrison.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        private void Start()
        {
            panel.SetActive(false);

            GameManager.Instance.GameStateChanged += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.GameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            if (state == GameState.GameOver)
                panel.SetActive(true);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}