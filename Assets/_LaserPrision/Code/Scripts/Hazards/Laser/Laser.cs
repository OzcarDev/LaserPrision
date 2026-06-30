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

        

        private Action<Laser> _releaseAction;

        private void Awake()
        {
            warningVisual.SetActive(false);
            beamVisual.SetActive(false);
        }

        
        public void SetReleaseAction(Action<Laser> releaseAction)
        {
            _releaseAction = releaseAction;
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


            Release();
        }

        private void EnableLaser()
        {
            beamVisual.SetActive(true);
        }

        private void DisableLaser()
        {
            beamVisual.SetActive(false);
        }

        private void Release()
        {
            _releaseAction?.Invoke(this);
        }

        

        public void OnSpawn()
        {

            warningVisual.SetActive(false);
            beamVisual.SetActive(false);

            StopAllCoroutines();
        }

        public void OnDespawn()
        {
            warningVisual.SetActive(false);
            beamVisual.SetActive(false);

            StopAllCoroutines();
        }

        
    }
}