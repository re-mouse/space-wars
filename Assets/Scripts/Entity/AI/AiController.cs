using System;
using Interfaces;
using UnityEngine;

namespace SpaceWars.SpaceShips.AI
{
    public class AiController : MonoBehaviour, IEntityController
    {
        private IShootable shootable;
        private IMovable movable;
        private IRotatable rotatable;
        private Transform target;
        public void SetShootable(IShootable shootable)
        {
            this.shootable = shootable;
        }

        public void SetRotatable(IRotatable rotatable)
        {
            this.rotatable = rotatable;
        }

        public void SetMovable(IMovable movable)
        {
            this.movable = movable;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
        
        private void Update()
        {
            if (target == null) return;
            
            if (movable as Component != null && Vector3.Distance(movable.GetPosition(), target.position) > 2)
                movable.Move(((Vector2)(target.position - movable.GetPosition())).normalized);
            
            if (shootable as Component != null)
                shootable.Shoot();

            if (rotatable as Component != null)
            {
                Vector3 direction = target.position - rotatable.GetPosition();
                float targetAngle = Vector2.SignedAngle(Vector2.right, direction);
                rotatable.RotateTo(targetAngle);
            }
        }
    }
}