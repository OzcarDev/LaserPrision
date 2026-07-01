using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LaserPrison.Player
{
    public class PlayerInputReader : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }

        public event Action InvulnerabilityPressed;

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        public void OnInvulnerability(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                InvulnerabilityPressed?.Invoke();
            }
        }
    }
}