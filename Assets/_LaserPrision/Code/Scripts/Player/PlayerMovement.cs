using UnityEngine;

namespace LaserPrison.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInputReader))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private CharacterController _controller;
        private PlayerInputReader _input;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<PlayerInputReader>();
        }

        private void Update()
        {
            Vector2 input = _input.MoveInput;

            Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

            _controller.Move(direction * moveSpeed * Time.deltaTime);
        }
    }
}