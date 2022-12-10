using UnityEngine;

namespace SpaceWars
{
    public interface IMovable
    {
        public void Move(Vector2 direction);
        public Vector3 GetPosition();
    }
}