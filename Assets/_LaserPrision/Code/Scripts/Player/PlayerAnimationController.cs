
using UnityEngine;

namespace LaserPrison.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform visual;
        [SerializeField] private float rotationSpeed = 15f;

        private static readonly int SpeedHash = Animator.StringToHash("Speed");

        private void Update()
        {
            UpdateAnimation();
            UpdateRotation();
        }

        private void UpdateAnimation()
        {
            animator.SetFloat(
                SpeedHash,
                movement.Velocity.magnitude);
        }

        private void UpdateRotation()
        {
            Vector3 direction = movement.Velocity;

            direction.y = 0f;

            if (direction.sqrMagnitude < 0.001f)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            visual.rotation = Quaternion.Slerp(
                visual.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
        }
    }
}