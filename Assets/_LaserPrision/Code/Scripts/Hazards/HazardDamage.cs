using LaserPrison.Interfaces;
using UnityEngine;

namespace LaserPrison.Hazards
{
    public class HazardDamage : MonoBehaviour
    {
        [SerializeField] private int damage = 1;

        private bool _hasDamaged;

        private void OnEnable()
        {
            _hasDamaged = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_hasDamaged)
                return;

            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damage);
                _hasDamaged = true;
                Debug.Log("HazardDamage: Dealt " + damage + " damage to " + other.name);
            }
        }
    }
}
    
