using System;
using Interfaces;

namespace SpaceWars
{
    public class GameManager : IStartGameStrategy, IEndGameStrategy
    {
        public static GameManager Instance { get; private set; }

        public IEndGameStrategy endGameStrategy;
        public IStartGameStrategy startGameStrategy;

        public GameManager(IEndGameStrategy endGameStrategy, IStartGameStrategy startGameStrategy)
        {
            if (endGameStrategy == null || startGameStrategy == null)
                throw new NullReferenceException();
            Instance = this;

            this.endGameStrategy = endGameStrategy;
            this.startGameStrategy = startGameStrategy;
        }

        public void StartGame()
        {
            startGameStrategy.StartGame();
        }

        public void EndGame()
        {
            endGameStrategy.EndGame();
        }
    }
}