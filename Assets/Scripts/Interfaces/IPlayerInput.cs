using UnityEngine;

namespace SpaceWars.Input
{
    public interface IPlayerInput
    {
        public Vector2 GetMovementDirection();
        public bool IsShootButtonPressed();
        public Vector3 GetCursorWorldPosition();
    }
}