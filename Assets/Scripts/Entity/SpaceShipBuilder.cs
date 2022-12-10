using System;
using SpaceWars.Cannons;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SpaceWars.SpaceShips
{
    public class SpaceShipBuilder
    {
        private SpaceShip spaceShip;
        private Cannon[] cannons;
        private SpaceShipModule[] modules;
        private Action onDestroyAction;

        private SpaceShip buildedSpaceship;
        
        public void SetSpaceShip(SpaceShip spaceShip)
        {
            this.spaceShip = spaceShip;
        }

        public void SetCannons(Cannon[] cannons)
        {
            this.cannons = cannons;
        }

        public void SetModules(SpaceShipModule[] modules)
        {
            this.modules = modules;
        }

        public void SetOnDestroy(Action onDestroy)
        {
            onDestroyAction = onDestroy;
        }

        public bool IsReadyToBuild()
        {
            return spaceShip != null;
        }

        public SpaceShip Build()
        {
            buildedSpaceship = Object.Instantiate(spaceShip);
            Cannon[] clonedCannons = new Cannon[cannons.Length];
            for (int i = 0; i < cannons.Length; i++)
                clonedCannons[i] = cannons[i].Clone(null);
            
            buildedSpaceship.Initialize(clonedCannons, modules, onDestroyAction);
            return buildedSpaceship;
        }
        
        public void DestroySpaceShip()
        {
            if (buildedSpaceship != null)
                Object.Destroy(buildedSpaceship.gameObject);
        }
    }
}