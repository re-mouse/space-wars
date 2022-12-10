using UnityEngine;

namespace SpaceWars.SpaceShips.Module
{
    [CreateAssetMenu(fileName = "Fire Speed", menuName = "Modules/Fire Speed")]
    public class FireSpeedPercentageModule: SpaceShipModule
    {
        [SerializeField]
        private float fireSpeedMultiplier;

        public override void Apply(SpaceShip spaceShip)
        {
            var stat = new StatModifier(StatModifierType.Percentage, fireSpeedMultiplier);
            foreach (var cannon in spaceShip.GetCannons())
                cannon.GetModel().RecoveryTime.AddModifier(stat);
        }
    }
}