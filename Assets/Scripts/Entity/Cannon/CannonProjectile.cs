using System;
using SpaceWars;
using SpaceWars.SpaceShips;
using UnityEngine;

namespace SpaceWars.Cannons
{
    public class CannonProjectile : MonoBehaviour, IPoolable<CannonProjectile>
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private float lifeTime;
        
        private int damage;
        private float activeTime;

        private Action<CannonProjectile> returnToPool;
        private IDamageable owner;

        public void SetDamage(int damage)
        {
            this.damage = damage;
        }

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public CannonProjectile Clone(Transform parent)
        {
            CannonProjectile newProjectile = Instantiate(this, parent);
            newProjectile.Disable();
            return newProjectile;
        }

        public void SetOwner(IDamageable owner)
        {
            this.owner = owner;
        }
        
        private void Update()
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            activeTime += Time.deltaTime;
            if (activeTime > lifeTime)
            {
                returnToPool(this);
                Disable();
            }
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable) && owner != damageable)
            {
                damageable.Damage(damage);
                returnToPool(this);
                Disable();
            }
        }

        public void SetPopToPool(Action<CannonProjectile> onReturnToPool)
        {
            returnToPool = onReturnToPool;
        }

        public void OnGetPulledFromPool()
        {
            Activate();
        }
    }
}