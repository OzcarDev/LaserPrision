using UnityEngine;
using LaserPrison.Gameplay;
using LaserPrison.Core;
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

        private Vector3 _startPosition;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<PlayerInputReader>();

            Debug.Assert(gameArea != null, "GameArea is not assigned in PlayerMovement.");

            _startPosition = transform.position;
        }

        private void Start()
        {
            if (GameManager.Instance != null) GameManager.Instance.GameSessionReset += ResetPlayer;
        }

        private void OnDisable()
        {
            if (GameManager.Instance != null) GameManager.Instance.GameSessionReset -= ResetPlayer;
        }

        private void Update()
        {
            if(GameManager.Instance.CurrentState != GameState.Playing)
            {
                Velocity = Vector3.zero;
                return;
            }
            Move();
        }

        private void Move()
        {
            Vector2 input = _input.MoveInput;

            if (input == Vector2.zero) 
            { 
                Velocity = Vector2.zero;
                return;
            } 

            Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

            _controller.Move(direction * moveSpeed * Time.deltaTime);

            Vector3 position = gameArea.ClampPosition(transform.position);

            transform.position = position;

            Velocity = _controller.velocity;
        }

        private void ResetPlayer()
        {
            _controller.enabled = false;

            transform.position = _startPosition;

            Velocity = Vector3.zero;

            _controller.enabled = true;
        }
    }
}