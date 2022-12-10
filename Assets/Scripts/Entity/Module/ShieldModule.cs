using UnityEngine;

namespace SpaceWars.SpaceShips.Module
{
    [CreateAssetMenu(fileName = "Shield", menuName = "Modules/Shield")]
    public class ShieldModule: SpaceShipModule
    {
        [SerializeField]
        private float shieldUpgrade;

        public override void Apply(SpaceShip spaceShip)
        {
            var stat = new StatModifier(StatModifierType.Flat, shieldUpgrade);
            spaceShip.GetModel().MaxShield.AddModifier(stat);
        }
    }
}