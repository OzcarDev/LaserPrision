using UnityEngine;

namespace LaserPrison.Gameplay
{
    [RequireComponent(typeof(BoxCollider))]
    public class GameArea : MonoBehaviour
    {
        private BoxCollider _gameAreaCollider;

        private Bounds _bounds;

        private void Awake()
        {
            _gameAreaCollider = GetComponent<BoxCollider>();
            _bounds = _gameAreaCollider.bounds;
        }

        public Vector3 ClampPosition(Vector3 position)
        {
            

            position.x = Mathf.Clamp(position.x, _bounds.min.x, _bounds.max.x);
            position.z = Mathf.Clamp(position.z, _bounds.min.z, _bounds.max.z);

            return position;
        }

        public Vector3 GetRandomPosition()
        {
            

            return new Vector3(
                Random.Range(_bounds.min.x, _bounds.max.x),
                0f,
                Random.Range(_bounds.min.z, _bounds.max.z));
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
           
            if (_gameAreaCollider == null)return;

            Gizmos.color = Color.green;

            Gizmos.matrix = transform.localToWorldMatrix;

            Gizmos.DrawWireCube(
                _gameAreaCollider.center,
                _gameAreaCollider.size);
        }
#endif
    }
}