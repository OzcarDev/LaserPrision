using System;
using System.Collections;
using UnityEngine;
using LaserPrison.Interfaces;

namespace LaserPrison.Hazards
{
    public class Laser : MonoBehaviour, IPoolable
    {
        [Header("Timing")]
        [SerializeField] private float warningTime = 1f;
        [SerializeField] private float activeTime = 0.2f;

        [Header("Visuals")]
        [SerializeField] private GameObject warningVisual;
        [SerializeField] private GameObject beamVisual;

        


        private void Awake()
        {
           ResetLaser();
        }
 
        public void Activate(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);

            StopAllCoroutines();

            StartCoroutine(LaserRoutine());
        }

        private IEnumerator LaserRoutine()
        {
            warningVisual.SetActive(true);

            yield return new WaitForSeconds(warningTime);

            warningVisual.SetActive(false);

            EnableLaser();

            yield return new WaitForSeconds(activeTime);

            DisableLaser();
        }

        private void EnableLaser()
        {
            beamVisual.SetActive(true);
        }

        private void DisableLaser()
        {
            beamVisual.SetActive(false);
        }

        public void OnSpawn()
        {
            ResetLaser();
        }

        public void OnDespawn()
        {
            ResetLaser();
        }

        private void ResetLaser()
        {
            StopAllCoroutines();

            warningVisual.SetActive(false);
            beamVisual.SetActive(false);

        }
    }
}