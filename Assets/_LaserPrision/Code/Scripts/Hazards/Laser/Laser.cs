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

        [SerializeField] private GameObject warningVisual;
        [SerializeField] private GameObject beamVisual;

        private LaserState _state;

        private void Awake()
        {
            warningVisual.SetActive(false);
            beamVisual.SetActive(false);
        }
        private void Start()
        {
            StartCoroutine(LaserRoutine());
        }

        private IEnumerator LaserRoutine()
        {
            _state = LaserState.Warning;
            warningVisual.SetActive(true);
            yield return new WaitForSeconds(warningTime);

            warningVisual.SetActive(false);
            _state = LaserState.Firing;
            

            EnableLaser();

            yield return new WaitForSeconds(activeTime);

            DisableLaser();
            _state = LaserState.Cooldown;

            Destroy(gameObject);
        }

        private void EnableLaser()
        {
            beamVisual.SetActive(true);
        }

        private void DisableLaser()
        {
            beamVisual.SetActive(false);
        }
    }
}