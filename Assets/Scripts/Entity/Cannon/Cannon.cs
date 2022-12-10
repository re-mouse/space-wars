using System;
using SpaceWars;
using UnityEngine;

namespace SpaceWars.Cannons
{
    public class Cannon : MonoBehaviour, ICloneable<Cannon>, IDescribed
    {
        [SerializeField]
        private CannonModel model;
        private IPool<CannonProjectile> projectilePool;
        private float lastShootTime;
        private IDamageable owner;
        
        public void Initialize(Transform parent, Vector3 localPosition)
        {
            owner = parent.GetComponent<IDamageable>();
            transform.parent = parent;
            transform.localPosition = localPosition;
            projectilePool = new CloneableObjectPool<CannonProjectile>(model.Projectile);
        }

        public void ShootIfReady()
        {
            if (!IsReady()) return;

            var projectile = projectilePool.Pull();
            projectile.Activate();
            projectile.SetOwner(owner);
            projectile.SetPositionAndRotation(transform.position, transform.rotation);
            projectile.SetDamage(model.Damage);
            lastShootTime = Time.timeSinceLevelLoad;
        }

        public CannonModel GetModel()
        {
            return model;
        }

        private bool IsReady()
        {
            return lastShootTime + model.RecoveryTime < Time.timeSinceLevelLoad;
        }

        public string GetDescribe()
        {
            return model.Describe;
        }

        public Cannon Clone(Transform parent)
        {
            return Instantiate(this, parent);
        }
    }
}