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

        private LaserState _state;

        private Action<Laser> _releaseAction;

        private void Awake()
        {
            warningVisual.SetActive(false);
            beamVisual.SetActive(false);
        }

        /// <summary>
        /// Se llama una única vez cuando el objeto es creado por el pool.
        /// </summary>
        public void SetReleaseAction(Action<Laser> releaseAction)
        {
            _releaseAction = releaseAction;
        }

        /// <summary>
        /// Se llama cada vez que el láser sale del pool.
        /// </summary>
        public void Activate(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);

            StopAllCoroutines();

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

        #region IPoolable

        public void OnSpawn()
        {
            _state = LaserState.Warning;

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

        #endregion
    }
}