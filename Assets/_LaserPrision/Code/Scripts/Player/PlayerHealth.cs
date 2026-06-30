using System;
using UnityEngine;
using LaserPrison.Interfaces;
namespace LaserPrison.Player
{
    public class PlayerHealth : MonoBehaviour,IDamageable
    {
        [SerializeField] private int maxLives = 3;
        [SerializeField] private PlayerInvulnerability playerInvulnerability;
        public int CurrentLives { get; private set; }

        public event Action<int> LivesChanged;
        public event Action Damaged;
        public event Action Died;

        private void Awake()
        {
            ResetHealth();
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
        public void ResetHealth()
        {
            CurrentLives = maxLives;
            LivesChanged?.Invoke(CurrentLives);
        }
    }
}