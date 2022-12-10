using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceWars.SpaceShips
{
    [CreateAssetMenu(fileName = "SpaceShipModel")]
    public class SpaceShipModel : ScriptableObject
    {
        public float Health { get; private set; }
        public float Shield { get; private set; }
        [field: SerializeField]
        public ModifiableStat MaxHealth { get ; private set; }
        [field: SerializeField]
        public ModifiableStat MaxShield { get; private set; }
        [field: SerializeField]
        public ModifiableStat ShieldRegenerationPerSecond  { get; private set; }
        [field: SerializeField]
        public int ModuleCounts  { get; private set; }
        [field: SerializeField]
        public int CannonCounts  { get; private set; }
        [field: SerializeField]
        public ModifiableStat Speed  { get; private set; }
        [field: SerializeField]
        public string Describe { get; private set; }

        public void Reset()
        {
            Shield = MaxShield;
            Health = MaxHealth;
        }

        public void Damage(int damage)
        {
            Shield -= damage;

            if (Shield < 0)
                Health += Shield;
        }

        public bool IsDead()
        {
            return Health < 0;
        }

        public void RegenerateShield(float time)
        {
            Shield += ShieldRegenerationPerSecond * time;
            if (Shield > MaxShield)
                Shield = MaxShield;
        }
        
        public SpaceShipModel Clone() =>
            (SpaceShipModel)this.MemberwiseClone();
    }
}