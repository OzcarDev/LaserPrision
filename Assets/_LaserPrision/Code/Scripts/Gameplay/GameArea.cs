using UnityEngine;

namespace LaserPrison.Gameplay
{
    [RequireComponent(typeof(BoxCollider))]
    public class GameArea : MonoBehaviour
    {
        private BoxCollider _gameAreaCollider;

        private Bounds _bounds => _gameAreaCollider.bounds;

        private void Awake()
        {
            _gameAreaCollider = GetComponent<BoxCollider>();
        }

        public Vector3 ClampPosition(Vector3 position)
        {
            

            position.x = Mathf.Clamp(position.x, _bounds.min.x, _bounds.max.x);
            position.z = Mathf.Clamp(position.z, _bounds.min.z, _bounds.max.z);

            return position;
        }

        public Vector3 GetRandomPosition(float y = 0f)
        {
            

            return new Vector3(
                Random.Range(_bounds.min.x, _bounds.max.x),
                y,
                Random.Range(_bounds.min.z, _bounds.max.z));
        }
    }
}