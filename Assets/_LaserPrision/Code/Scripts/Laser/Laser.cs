using System.Collections;
using UnityEngine;
using LaserPrison.Interfaces;

namespace LaserPrison.Hazards
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private float warningTime = 1f;
        [SerializeField] private float activeTime = 0.2f;
        [SerializeField] private int damage = 1;

        private LaserState _state;

        private void Start()
        {
            StartCoroutine(LaserRoutine());
        }

        private IEnumerator LaserRoutine()
        {
            _state = LaserState.Warning;
            yield return new WaitForSeconds(warningTime);

            _state = LaserState.Firing;

            Fire();

            yield return new WaitForSeconds(activeTime);

            _state = LaserState.Cooldown;

            Destroy(gameObject);
        }

        private void Fire()
        {
            Ray ray = new Ray(transform.position, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, 20f))
            {
                if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.TakeDamage(damage);
                }
            }
        }
    }
}