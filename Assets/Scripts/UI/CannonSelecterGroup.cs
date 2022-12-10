using System;
using System.Collections.Generic;
using SpaceWars.Cannons;
using UnityEngine;

namespace SpaceWars.UI
{
    public class CannonSelecterGroup : SelecterGroup<Cannon>, ICloneable<CannonSelecterGroup>
    {
        public CannonSelecterGroup Clone(Transform parent)
        {
            return Instantiate(this, parent);
        }
    }
}