using Interfaces;
using SpaceWars.SpaceShips;
using SpaceWars.SpaceShips.AI;
using SpaceWars.UI;
using UnityEngine;

namespace SpaceWars
{
    public class ShipsFightStartStrategy : IStartGameStrategy
    {
        private IEntityController playerController;
        private AiController aiController;
        private SpaceShipBuilder playerBuilder;
        private SpaceShipBuilder aiBuilder;
        private Vector3 playerStartPosition;
        private Vector3 aiStartPosition;
        private IHideable[] hideablesMenu;

        public ShipsFightStartStrategy(IHideable[] hideablesMenu, IEntityController playerController, AiController aiController)
        {   
            this.playerController = playerController;
            this.aiController = aiController;
            this.hideablesMenu = hideablesMenu;
        }

        public void SetPlayerBuilder(SpaceShipBuilder spaceShipBuilder)
        {
            this.playerBuilder = spaceShipBuilder;
        }

        public void SetAIBuilder(SpaceShipBuilder spaceShipBuilder)
        {
            this.aiBuilder = spaceShipBuilder;
        }

        public void SetAIPosition(Vector3 startPosition)
        {
            aiStartPosition = startPosition;
        }
        
        public void SetPlayerPosition(Vector3 startPosition)
        {
            playerStartPosition = startPosition;
        }

        public void StartGame()
        {
            if (!playerBuilder.IsReadyToBuild() || !aiBuilder.IsReadyToBuild())
                return;
            
            foreach (var hideable in hideablesMenu)
                hideable.Hide();
            
            SpaceShip playerSpaceShip = playerBuilder.Build();
            playerSpaceShip.transform.position = playerStartPosition;
            playerController.SetMovable(playerSpaceShip);
            playerController.SetRotatable(playerSpaceShip);
            playerController.SetShootable(playerSpaceShip);

            SpaceShip aiSpaceShip = aiBuilder.Build();
            aiSpaceShip.transform.position = aiStartPosition;
            aiController.SetMovable(aiSpaceShip);
            aiController.SetRotatable(aiSpaceShip);
            aiController.SetShootable(aiSpaceShip);
            aiController.SetTarget(playerSpaceShip.transform);
        }
    }
}