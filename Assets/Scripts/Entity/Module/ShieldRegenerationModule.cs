using UnityEngine;

namespace SpaceWars.SpaceShips.Module
{
    [CreateAssetMenu(fileName = "Shield Recovery", menuName = "Modules/Shield Recovery")]
    public class ShieldRegenerationModule : SpaceShipModule
    {
        [SerializeField]
        private float regenerationPerSecondsMultiplier;

        public override void Apply(SpaceShip spaceShip)
        {
            var stat = new StatModifier(StatModifierType.Percentage, regenerationPerSecondsMultiplier);
            spaceShip.GetModel().ShieldRegenerationPerSecond.AddModifier(stat);
        }
    }
}