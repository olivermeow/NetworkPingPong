using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Gameplay.Player;
using Infarastructure.Services;
using Infrastucture.StateMachine;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infarastructure.GameStateMachine.States
{
    public class GameLoopState : IPayloadedState<GameStateParams>
    {
        private GameLoopService _gameLoopService;
        private GameFactory _gameFactory;
        private ServiceProvider<GameLoopService> _gameLoopProvider;
        private ServiceProvider<GameFactory> _gameFactoryProvider;


        public GameLoopState(ServiceProvider<GameLoopService> gameLoopProvider, ServiceProvider<GameFactory> gameFactoryProvider)
        {
            _gameLoopProvider = gameLoopProvider;
            _gameFactoryProvider = gameFactoryProvider;
        }

        public void Enter(GameStateParams payload)
        {
            _gameFactory = _gameFactoryProvider.Get();
            
            List<GameObject> players = _gameFactory.SpawnAllPlayers(payload.Players);
            List<PlayerScore> playerScores = new List<PlayerScore>();
            
            _gameFactory.SpawnBall();

            foreach (var playerGameobject in players)
            {
                var playerScore = playerGameobject.GetComponent<PlayerScore>();
                playerScores.Add(playerScore);
            }
            
            _gameLoopService = _gameLoopProvider.Get();
            
            _gameLoopService.Initialize(playerScores);
        }

        public void Exit()
        {
            
        }
    }
}
