using System;
using System.Collections.Generic;
using System.Diagnostics;
using SpaceWars.Cannons;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace SpaceWars.SpaceShips
{
    public class SpaceShip : MonoBehaviour, IMovable, IRotatable, IShootable, IDamageable, IDescribed
    {
        [SerializeField]
        private float rotateSpeed;
        [SerializeField]
        private SpaceShipModel model;

        [SerializeField]
        private Transform[] cannonSlots;

        private Cannon[] cannons;
        private SpaceShipModule[] modules;
        private int initializedCannonsCount;
        private Action onDestroy;
        private float euler;

        public void Initialize(Cannon[] cannons, SpaceShipModule[] modules, Action onDestroyAction)
        {
            if (cannons.Length > model.CannonCounts || cannons.Length > cannonSlots.Length)
            {
                Debug.LogError("Trying initialize more cannons, than cannon space count");
            }

            if (modules.Length > model.ModuleCounts)
            {
                Debug.LogError("Trying initialize more modules on spaceship, than space available");
                return;
            }

            model = model.Clone();
            model.Reset();
            this.cannons = cannons;
            this.modules = modules;
            
            foreach (var cannon in cannons)
                InitializeCanon(cannon);
            foreach (var module in modules)
                InitializeModule(module);

            onDestroy = onDestroyAction;
        }
    
        public void Move(Vector2 direction)
        {
            direction = direction.normalized;
            Vector3 newPosition = transform.position;
            newPosition.x += direction.x * model.Speed * Time.deltaTime;
            newPosition.y += direction.y * model.Speed * Time.deltaTime;
            transform.position = newPosition;
        }

        public void LookAt(Vector3 target)
        {
            transform.LookAt(target);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void RotateTo(float value)
        {
            Vector3 newRotation = transform.root.eulerAngles;
            newRotation.z = Mathf.SmoothDampAngle(newRotation.z, value, ref euler, rotateSpeed);;
            transform.rotation = Quaternion.Euler(newRotation);
        }

        public void Shoot()
        {
            foreach (var cannon in cannons)
                cannon.ShootIfReady();
        }
    
        public void Damage(int damage)
        {
            model.Damage(damage);
            if (model.IsDead())
            {
                onDestroy(); 
            }
        }
    
        public IReadOnlyCollection<Cannon> GetCannons()
        {
            return cannons;
        }
        
        public string GetDescribe()
        {
            return model.Describe;
        }

        public SpaceShipModel GetModel()
        {
            return model;
        }
        
        private void InitializeCanon(Cannon cannon)
        {
            cannon.Initialize(transform, cannonSlots[initializedCannonsCount].localPosition);
            initializedCannonsCount++;
        }

        private void InitializeModule(SpaceShipModule module)
        {
            module.Apply(this);
        }

        private void Update()
        {
            model.RegenerateShield(Time.deltaTime);
        }
    }
}
