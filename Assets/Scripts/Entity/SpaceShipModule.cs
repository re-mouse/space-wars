using UnityEngine;

namespace SpaceWars.SpaceShips
{
    public abstract class SpaceShipModule : ScriptableObject, IDescribed
    {
        [field: SerializeField]
        public string Describe { get; private set; }
        
        public abstract void Apply(SpaceShip spaceShip);
        
        public string GetDescribe()
        {
            return Describe;
        }
    }
}