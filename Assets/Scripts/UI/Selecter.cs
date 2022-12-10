using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SpaceWars.UI
{
    public class Selecter : MonoBehaviour, ICloneable<Selecter>, IHideable
    {
        [SerializeField]
        private TextMeshProUGUI describeContainer;

        [SerializeField]
        private Color selectedColor;

        [SerializeField]
        private Color unSelectedColor;
        [SerializeField]
        private Color unactiveColor;

        [SerializeField]
        private Button button;

        private Action onSelect;
        private Action onUnselect;

        private bool isSelected;
        private bool canBeSelected;
        
        public void Initialize(Action onSelect, Action onUnselect)
        {
            this.onSelect = onSelect;
            this.onUnselect = onUnselect;
            button.onClick.AddListener(OnClick);
        }

        public void SetActive(bool canBeSelected)
        {
            this.canBeSelected = canBeSelected;
            UpdateButtonColor();
        }

        public void Reset()
        {
            canBeSelected = true;
            isSelected = false;
            UpdateButtonColor();
        }
        
        public Selecter Clone(Transform parent)
        {
            return Instantiate(GetComponent<Selecter>(), parent);
        }

        public void SetDescribe(string text)
        {
            describeContainer.text = text;
        }

        private void OnClick()
        {
            if (!canBeSelected)
                return;
            
            if (isSelected)
                onUnselect();
            else
                onSelect();

            isSelected = !isSelected;
            UpdateButtonColor();
        }

        private void UpdateButtonColor()
        {
            if (!canBeSelected)
            {
                button.image.color = unactiveColor;
                return;
            }

            button.image.color = isSelected ? selectedColor : unSelectedColor;
        }

        public void Hide()
        {
            button.gameObject.SetActive(false);
        }

        public void Show()
        {
            button.gameObject.SetActive(true);
        }
    }
}