using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceWars
{
    [Serializable]
    public class ModifiableStat
    {
        public float Value
        {
            get
            {
                if (!isCalculated)
                    CalculateValue();
                return calculatedValue;
            }
            private set { calculatedValue = value; }
        }

        [SerializeField]
        private float value;
        private float calculatingValue;
        private float calculatingMultiplyPercents = 1;
        private bool isCalculated = false;
        private List<StatModifier> modifiers = new List<StatModifier>();

        private float calculatedValue;

        public void AddModifier(StatModifier modifier)
        {
            modifiers.Add(modifier);
            CalculateValue();
        }

        private void RemoveModifier(StatModifier modifier)
        {
            modifiers.Remove(modifier);
            CalculateValue();
        }

        private void CalculateValue()
        {
            calculatingMultiplyPercents = 1;
            calculatingValue = value;
            foreach (var modifier in modifiers)
                ApplyModifier(modifier);

            Value = calculatingValue * calculatingMultiplyPercents;
        }

        private void ApplyModifier(StatModifier modifier)
        {
            if (modifier.Type == StatModifierType.Flat)
                calculatingValue += modifier.Value;
            else if (modifier.Type == StatModifierType.Percentage)
                calculatingMultiplyPercents += modifier.Value;
        }
        
        public static implicit operator int(ModifiableStat stat) => Mathf.RoundToInt(stat.Value);
        public static implicit operator float(ModifiableStat stat) => stat.Value;
    }

    public class StatModifier
    {
        public float Value { get; }
        public StatModifierType Type { get; }

        public StatModifier(StatModifierType type, float value)
        {
            Type = type;
            Value = value;
        }
    }

    public enum StatModifierType
    {
        Flat,
        Percentage
    }
}