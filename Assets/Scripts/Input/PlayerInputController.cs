using System;
using Interfaces;
using SpaceWars;
using UnityEngine;

namespace SpaceWars.Input
{
    public class PlayerInputController : MonoBehaviour, IEntityController
    {
        private IPlayerInput input;
        private IMovable movable;
        private IRotatable rotatable;
        private IShootable shootable;

        private bool isInitialized;

        public void SetInput(IPlayerInput input)
        {
            this.input = input;
            isInitialized = true;
        }

        private void Update()
        {
            if (!isInitialized) return;

            if (movable as Component != null)
                movable.Move(input.GetMovementDirection());

            if (shootable  as Component != null && input.IsShootButtonPressed())
                shootable.Shoot();

            if (rotatable  as Component != null)
            {
                var rot = Quaternion.LookRotation(rotatable.GetPosition() - input.GetCursorWorldPosition(), Vector3.forward);
                rotatable.RotateTo(rot.eulerAngles.z + 90);
            }
        }

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
    }
}