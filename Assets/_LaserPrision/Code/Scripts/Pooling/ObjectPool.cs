using System;
using UnityEngine;
using UnityEngine.Pool;
using LaserPrison.Interfaces;

namespace LaserPrison.Pooling
{
    public abstract class ObjectPoolController<T> : MonoBehaviour
        where T : Component, IPoolable
    {
        [SerializeField] protected T prefab;

        [SerializeField] private int defaultCapacity = 10;
        [SerializeField] private int maxSize = 30;

        protected ObjectPool<T> Pool;

        protected virtual void Awake()
        {
            Pool = new ObjectPool<T>(
                CreateObject,
                OnGet,
                OnRelease,
                OnDestroyPoolObject,
                true,
                defaultCapacity,
                maxSize);
        }

        protected virtual T CreateObject()
        {
            T instance = Instantiate(prefab, transform);

            instance.gameObject.SetActive(false);

            return instance;
        }

        protected virtual void OnGet(T obj)
        {
            obj.gameObject.SetActive(true);
            obj.OnSpawn();
        }

        protected virtual void OnRelease(T obj)
        {
            obj.OnDespawn();
            obj.gameObject.SetActive(false);
        }

        protected virtual void OnDestroyPoolObject(T obj)
        {
            Destroy(obj.gameObject);
        }

        public T Get()
        {
            return Pool.Get();
        }

        public void Release(T obj)
        {
            Pool.Release(obj);
        }
    }
}