using SpaceWars.SpaceShips;
using UnityEngine;

namespace SpaceWars.UI
{
    public class SpaceShipSelecterGroup : SelecterGroup<SpaceShip>, ICloneable<SpaceShipSelecterGroup>
    {
        public SpaceShipSelecterGroup Clone(Transform parent)
        {
            return Instantiate(this, parent);
        }
    }
}