using System;

namespace SpaceWars
{
    public interface IPoolable<T> : ICloneable<T>
    {
        public void SetPopToPool(Action<T> onReturnToPool);
        public void OnGetPulledFromPool();
    }
}