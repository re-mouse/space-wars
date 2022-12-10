using Interfaces;
using SpaceWars.UI;
using UnityEngine.UI;

namespace SpaceWars
{
    public class HideableVerticalLayout : VerticalLayoutGroup, IHideable, IShowable
    {
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}