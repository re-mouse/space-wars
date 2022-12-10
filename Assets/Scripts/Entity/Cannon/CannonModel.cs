using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceWars.Cannons
{
    [CreateAssetMenu(fileName = "CannonModel")]
    public class CannonModel : ScriptableObject
    {
        [field: SerializeField]
        public ModifiableStat Damage { get; private set; }

        [field: SerializeField]
        public ModifiableStat RecoveryTime { get; private set; }

        [field: SerializeField]
        public CannonProjectile Projectile { get; private set; }
        [field: SerializeField]
        public string Describe { get; private set; }
        
        public CannonModel Clone() =>
            (CannonModel)this.MemberwiseClone();
    }
}