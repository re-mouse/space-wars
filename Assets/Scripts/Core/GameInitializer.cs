using System;
using System.Collections.Generic;
using Interfaces;
using SpaceWars;
using SpaceWars.Cannons;
using SpaceWars.Input;
using SpaceWars.SpaceShips;
using SpaceWars.SpaceShips.AI;
using SpaceWars.UI;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceWars
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField]
        private Transform playerStartPositionHolder;
        [SerializeField]
        private Transform aiStartPositionHolder;
        [SerializeField]
        private List<Cannon> availableCannons;
        [SerializeField]
        private List<SpaceShip> availableSpaceShips;
        [SerializeField]
        private List<SpaceShipModule> availableSpaceShipModules;

        [SerializeField]
        private CannonSelecterGroup cannonSelecterPrefab;
        [SerializeField]
        private SpaceShipSelecterGroup shipSelecterPrefab;
        [SerializeField]
        private ModuleSelecterGroup moduleSelecterPrefab;

        [SerializeField]
        private HideableVerticalLayout playerSelecterLayout;
        [SerializeField]
        private HideableVerticalLayout aiSelecterLayout;

        [SerializeField]
        private StartGameButton startGameButton;

        [SerializeField]
        private PlayerInputController inputController;
        [SerializeField]
        private AiController aiController;

        private void Start()
        {
            IPlayerInput input = new KeyBoardAndMouseInput();
            inputController.SetInput(input);

            SpaceShipBuilder playerBuilder = new SpaceShipBuilder();
            SpaceShipBuilder aiBuilder = new SpaceShipBuilder();

            SpaceShipUIFacade aiUIFacade = new SpaceShipUIFacade(aiSelecterLayout, shipSelecterPrefab, cannonSelecterPrefab, moduleSelecterPrefab);
            aiUIFacade.Initialize(aiBuilder, availableSpaceShips, availableCannons, availableSpaceShipModules);
            
            SpaceShipUIFacade playerUIFacade = new SpaceShipUIFacade(playerSelecterLayout, shipSelecterPrefab, cannonSelecterPrefab, moduleSelecterPrefab);
            playerUIFacade.Initialize(playerBuilder, availableSpaceShips, availableCannons, availableSpaceShipModules);
            
            MenuOpenEndStrategy menuOpenEndStrategy = new MenuOpenEndStrategy(new IShowable[]{playerSelecterLayout, aiSelecterLayout, startGameButton}, (
                () =>
                {
                    playerBuilder.DestroySpaceShip();
                    aiBuilder.DestroySpaceShip();
                }));
            
            ShipsFightStartStrategy startGameStrategy = new ShipsFightStartStrategy(new IHideable[] {aiSelecterLayout, playerSelecterLayout, startGameButton},
                inputController, aiController);
            
            startGameStrategy.SetPlayerBuilder(playerBuilder);
            startGameStrategy.SetAIBuilder(aiBuilder);
            startGameStrategy.SetPlayerPosition(playerStartPositionHolder.position);
            startGameStrategy.SetAIPosition(aiStartPositionHolder.position);
            
            GameManager gameManager = new GameManager(menuOpenEndStrategy, startGameStrategy);
            
            playerBuilder.SetOnDestroy(gameManager.EndGame);
            aiBuilder.SetOnDestroy(gameManager.EndGame);
            
            startGameButton.Initialize(gameManager);
        }
    }
}