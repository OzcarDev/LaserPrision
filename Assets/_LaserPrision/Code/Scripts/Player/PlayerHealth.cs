using LaserPrison.Core;
using LaserPrison.Interfaces;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace LaserPrison.Player
{
    public class PlayerHealth : MonoBehaviour,IDamageable
    {
        [Header("Settings")]
        [SerializeField] private int maxLives = 3;
        [Header("Dependencies")]
        [SerializeField] private PlayerInvulnerability playerInvulnerability;
        public int CurrentLives { get; private set; }

        public event Action<int> LivesChanged;
        public UnityEvent Damaged;
        public event Action Died;

        private void Awake()
        {
            Debug.Assert(playerInvulnerability != null, "PlayerInvulnerability is not assigned in PlayerHealth");

            ResetHealth();
        }
        private void Start()
        {
            if (GameManager.Instance != null) GameManager.Instance.GameSessionReset += ResetHealth;
        }

        private void OnDisable()
        {
            if (GameManager.Instance != null) GameManager.Instance.GameSessionReset -= ResetHealth;
        }
        public void TakeDamage(int damage)
        {
            if(!CanTakeDamage()) return;

            CurrentLives = Mathf.Max(CurrentLives - damage, 0);

            Damaged?.Invoke();
            LivesChanged?.Invoke(CurrentLives);

            if (CurrentLives == 0) Died?.Invoke();
        }

        private bool CanTakeDamage()
        {
            if (CurrentLives <= 0) return false;

            if (playerInvulnerability != null && playerInvulnerability.IsInvulnerable) return false;

            return true;
        }
        private void ResetHealth()
        {
            CurrentLives = maxLives;
            LivesChanged?.Invoke(CurrentLives);
        }
    }
}