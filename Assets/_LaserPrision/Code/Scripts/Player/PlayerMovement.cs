using UnityEngine;
using LaserPrison.Gameplay;
namespace LaserPrison.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInputReader))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        [SerializeField] private GameArea gameArea;

        private CharacterController _controller;
        private PlayerInputReader _input;

        public Vector3 Velocity { get; private set; }

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

            Vector3 position = transform.position;

            position = gameArea.ClampPosition(position);

            transform.position = position;

            Velocity = _controller.velocity;
        }
    }
}