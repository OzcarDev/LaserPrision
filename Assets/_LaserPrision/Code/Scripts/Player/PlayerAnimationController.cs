
using LaserPrison.Core;
using UnityEngine;

namespace LaserPrison.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float rotationSpeed = 15f;

        [Header("Dependencies")]
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform visual;

        private Quaternion _initialLocalRotation;

        private void Awake()
        {
            Debug.Assert(movement != null, "PlayerMovement is not assigned in Player Animation Controller");
            Debug.Assert(animator != null, "Animator is not assigned in Player Animation Controller");
            Debug.Assert(visual != null, "Visual Transform is not assigned in Player Animation Controller");

            _initialLocalRotation = visual.localRotation;
        }

        private void Start()
        {
            if (GameManager.Instance != null) GameManager.Instance.GameSessionReset += ResetPlayerRotation;
        }

        private void OnDisable()
        {
            if (GameManager.Instance != null) GameManager.Instance.GameSessionReset -= ResetPlayerRotation;
        }
        private void Update()
        {
            if (GameManager.Instance.CurrentState != GameState.Playing) return;

            UpdateAnimation();

            UpdateRotation();
        }

        private void UpdateAnimation()
        {
            animator.SetFloat( "Speed", movement.Velocity.magnitude);
        }

        private void UpdateRotation()
        {
            Vector3 direction = movement.Velocity;

            direction.y = 0f;

            if (direction.sqrMagnitude < 0.001f) return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            visual.rotation = Quaternion.Slerp( visual.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        private void ResetPlayerRotation()
        {
            visual.localRotation = _initialLocalRotation;

            animator.SetFloat("Speed", 0F);
        }
    }
}