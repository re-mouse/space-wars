using System;
using System.Collections.Generic;
using SpaceWars;
using UnityEngine;

namespace SpaceWars
{
    public class CloneableObjectPool<T> : IPool<T> where T : IPoolable<T>
    {
        private T cloneableObject;
        private Queue<T> pool = new Queue<T>();
        private Transform parent;
        
        public CloneableObjectPool(T cloneableObject, int startCount = 5)
        {
            this.cloneableObject = cloneableObject;
            for (int i = 0; i < startCount; i++)
                AddNewProjectileToPool();

            parent = new GameObject("Object pool").transform;
        }

        public void Pop(T projectile)
        {
            pool.Enqueue(projectile);
        }

        public T Pull()
        {
            if (pool.Count == 0)
                AddNewProjectileToPool();

            T projectile = pool.Dequeue();
            projectile.OnGetPulledFromPool();
            return projectile;
        }

        private void AddNewProjectileToPool()
        {
            T newProjectile = cloneableObject.Clone(parent);
            newProjectile.SetPopToPool(Pop);
            pool.Enqueue(newProjectile);
        }
    }
}