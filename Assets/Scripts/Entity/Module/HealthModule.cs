using UnityEngine;

namespace SpaceWars.SpaceShips.Module
{
    [CreateAssetMenu(fileName = "Health", menuName = "Modules/Health")]
    public class HealthModule : SpaceShipModule
    {
        [SerializeField]
        private float healthUpgrade;

        public override void Apply(SpaceShip spaceShip)
        {
            var stat = new StatModifier(StatModifierType.Flat, healthUpgrade);
            spaceShip.GetModel().MaxHealth.AddModifier(stat);
        }
    }
}