using LaserPrison.Player;
using System;
using UnityEngine;

namespace LaserPrison.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState CurrentState { get; private set; }

        public event Action<GameState> GameStateChanged;

        [SerializeField] private PlayerHealth playerHealth;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void OnEnable()
        {
            CurrentState = GameState.Waiting;
            GameStateChanged?.Invoke(CurrentState);

            playerHealth.Died += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            GameOver();
        }

        public void GameOver()
        {
            CurrentState = GameState.GameOver;
            GameStateChanged?.Invoke(CurrentState);
        }

        public void Restart()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
            );
        }
    }
}