using UnityEngine;

namespace SpaceWars
{
    public interface ICloneable<out T>
    {
        public T Clone(Transform parent);
    }
}