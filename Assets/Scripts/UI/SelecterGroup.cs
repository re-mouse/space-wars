using System;
using System.Collections.Generic;
using Interfaces;
using SpaceWars.Cannons;
using UnityEngine;

namespace SpaceWars.UI
{
    public abstract class SelecterGroup<T> : MonoBehaviour, IHideable, IShowable where T : IDescribed
    {
        [SerializeField]
        private Selecter selecterPrefab;

        private Action<List<T>> onSelectedUpdate;
        private List<T> selected = new List<T>();
        private Dictionary<Selecter, T> selectables = new Dictionary<Selecter, T>();
        private int maxCount;
        
        public void Initialize(Action<List<T>> onSelectedUpdate, List<T> selectableOptions)
        {
            this.onSelectedUpdate = onSelectedUpdate;
            foreach (var option in selectableOptions)
                CreateAndInitializeSelectable(option);
        }
        
        public void SetMaxSelectCount(int maxCount)
        {
            this.maxCount = maxCount;
            Reset();
        }

        private void Reset()
        {
            selected.Clear();
            ResetAllSelectables();
            
            if (maxCount == 0)
                DisableAllNonSelected();
            else
                EnableAll();
            
            onSelectedUpdate?.Invoke(selected);
        }

        private void CreateAndInitializeSelectable(T selectOption)
        {
            Selecter selecter = selecterPrefab.Clone(transform);
            selecter.Initialize(() => OnSelectedOption(selectOption), 
                () => OnUnSelectedOption(selectOption));
            selecter.SetDescribe(selectOption.GetDescribe());
            selectables[selecter] = selectOption;
        }

        private void OnSelectedOption(T option)
        {
            if (selected.Count > maxCount)
            {
                throw new Exception("Selectables reached max count");
            }
            
            selected.Add(option);
            onSelectedUpdate(selected);
            
            if (selected.Count == maxCount)
                DisableAllNonSelected();
        }
        
        private void OnUnSelectedOption(T option)
        {
            if (selected.Count == maxCount)
                EnableAll();
            selected.Remove(option);
            onSelectedUpdate(selected);
        }

        private void DisableAllNonSelected()
        {
            foreach (var optionSelectable in selectables)
            {
                if (selected.Contains(optionSelectable.Value))
                    continue;
                optionSelectable.Key.SetActive(false);
            }
        }
        
        private void EnableAll()
        {
            foreach (var optionSelectable in selectables)
            {
                optionSelectable.Key.SetActive(true);
            }
        }

        private void ResetAllSelectables()
        {
            foreach (var optionSelectable in selectables)
                optionSelectable.Key.Reset();
        }

        public void Hide()
        {
            foreach (var selectable in selectables.Keys)
                selectable.Hide();
        }

        public void Show()
        {
            foreach (var selectable in selectables.Keys)
                selectable.Show();
        }
    }
}