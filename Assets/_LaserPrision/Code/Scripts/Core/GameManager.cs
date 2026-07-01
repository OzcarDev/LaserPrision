using LaserPrison.Player;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LaserPrison.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public GameState CurrentState { get; private set; }

        public event Action<GameState> GameStateChanged;
        public event Action GameSessionReset;

        [Header("Dependencies")]
        [SerializeField] private PlayerHealth playerHealth;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            Debug.Assert(playerHealth != null,$"{nameof(PlayerHealth)} reference is missing.");

            ChangeState(GameState.Waiting);
        }

        private void OnEnable()
        {
            if (playerHealth != null) playerHealth.Died += GameOver;
           
        }

        private void OnDisable()
        {
            if (playerHealth != null) playerHealth.Died -= GameOver;
        }


        public void StartGame()
        {
            if (CurrentState != GameState.Waiting) return;

            ChangeState(GameState.Playing);
        }

        private void GameOver()
        {
            if (CurrentState != GameState.Playing) return;

            ChangeState(GameState.GameOver);
        }       

        public void ResetGameSession()
        {
           
            ChangeState(GameState.Waiting);
            GameSessionReset?.Invoke();
        }

        private void ChangeState(GameState newState)
        {
            if (CurrentState == newState) return;

            CurrentState = newState;
            GameStateChanged?.Invoke(CurrentState);
            Debug.Log("State changed: " + CurrentState);
            Debug.Log(GameStateChanged.GetInvocationList().Length.ToString());
        }
    }
}