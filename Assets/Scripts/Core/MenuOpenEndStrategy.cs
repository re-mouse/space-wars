using System;
using Interfaces;
using SpaceWars.UI;

namespace SpaceWars
{
    public class MenuOpenEndStrategy : IEndGameStrategy
    {
        private Action onGameEnded;
        private IShowable[] menuToOpen;
        public MenuOpenEndStrategy(IShowable[] menu, Action onEndGamended)
        {
            menuToOpen = menu;
            onGameEnded = onEndGamended;
        }
        
        public void EndGame()
        {
            foreach (var menu in menuToOpen)
                menu.Show();
            onGameEnded();
        }
    }
}