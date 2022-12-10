using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceWars.UI
{
    public class StartGameButton : MonoBehaviour, IShowable, IHideable
    {
        [SerializeField]
        private Button button;
        
        public void Initialize(IStartGameStrategy gameStrategy)
        {
            button.onClick.AddListener(gameStrategy.StartGame);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}