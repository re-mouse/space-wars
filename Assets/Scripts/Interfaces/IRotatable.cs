using UnityEngine;

namespace SpaceWars
{
    public interface IRotatable
    {
        public void RotateTo(float angle);
        public void LookAt(Vector3 position);
        public Vector3 GetPosition();
    }
}