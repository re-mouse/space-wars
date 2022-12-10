using SpaceWars.SpaceShips;
using UnityEngine;

namespace SpaceWars.UI
{
    public class ModuleSelecterGroup : SelecterGroup<SpaceShipModule>, ICloneable<ModuleSelecterGroup>
    {
        public ModuleSelecterGroup Clone(Transform parent)
        {
            return Instantiate(this, parent);
        }
    }
}