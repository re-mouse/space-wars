using System.Collections.Generic;
using System.Threading;
using Interfaces;
using SpaceWars.Cannons;
using SpaceWars.SpaceShips;
using UnityEngine.UI;

namespace SpaceWars.UI
{
    public class SpaceShipUIFacade : IShowable, IHideable
    {
        private SpaceShipSelecterGroup shipSelecter;
        private CannonSelecterGroup cannonSelecter;
        private ModuleSelecterGroup moduleSelecter;
        private SpaceShipBuilder builder;
        
        public SpaceShipUIFacade(VerticalLayoutGroup parent, SpaceShipSelecterGroup shipSelecter,
            CannonSelecterGroup cannonSelecter,
            ModuleSelecterGroup moduleSelecter)
        {
            this.shipSelecter = shipSelecter.Clone(parent.transform);
            this.cannonSelecter = cannonSelecter.Clone(parent.transform);
            this.moduleSelecter = moduleSelecter.Clone(parent.transform);
        }

        public void Initialize(SpaceShipBuilder builder, List<SpaceShip> spaceShips, List<Cannon> cannons, List<SpaceShipModule> modules)
        {
            this.builder = builder;
            
            shipSelecter.Initialize(OnSpaceShipSelected, spaceShips);
            cannonSelecter.Initialize(OnCannonsSelected, cannons);
            moduleSelecter.Initialize(OnModulesSelected, modules);
            
            shipSelecter.SetMaxSelectCount(1);
            cannonSelecter.SetMaxSelectCount(0);
            moduleSelecter.SetMaxSelectCount(0);
        }

        private void OnSpaceShipSelected(List<SpaceShip> spaceShips)
        {
            if (spaceShips.Count == 0)
            {
                cannonSelecter.SetMaxSelectCount(0);
                moduleSelecter.SetMaxSelectCount(0);

                return;
            }

            cannonSelecter.SetMaxSelectCount(spaceShips[0].GetModel().CannonCounts);
            moduleSelecter.SetMaxSelectCount(spaceShips[0].GetModel().ModuleCounts);
            builder.SetSpaceShip(spaceShips[0]);
        }

        private void OnCannonsSelected(List<Cannon> selectedCannons)
        {
            builder.SetCannons(selectedCannons.ToArray());
        }
        
        private void OnModulesSelected(List<SpaceShipModule> selectedModules)
        {
            builder.SetModules(selectedModules.ToArray());
        }

        public void Show()
        {
            cannonSelecter.Show();
            shipSelecter.Show();
            moduleSelecter.Show();
        }

        public void Hide()
        {
            cannonSelecter.Hide();
            shipSelecter.Hide();
            moduleSelecter.Hide();
        }
    }
}