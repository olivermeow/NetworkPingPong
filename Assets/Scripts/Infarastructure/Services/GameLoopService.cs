using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Player;
using Gameplay.UI;
using Infarastructure.GameStateMachine.States;
using Infarastructure.Network;
using Infarastructure.StaticData;
using Infrastucture.StateMachine;
using Mirror;
using QFSW.QC;
using UnityEngine;
using Zenject;

namespace Infarastructure.Services
{
    public class GameLoopService : NetworkBehaviour
    {
        [SerializeField] private ResultWindow resultWindow;
        [SerializeField] private RoundWindow _roundWindow;
        private List<PlayerScore> _playerScores;
        private ServiceProvider<GameLoopService> _gameloopServiceProvider;
        private GameFactory _gameFactory;
        private GameSettings _gameSettings;
        private GameStateMachine.GameStateMachine _gameStateMachine;

        public event Action PlayerWin;

        private Dictionary<NetworkConnectionToClient, int> playersWinnedRounds;
        
        [SyncVar] private int Round;

        [Inject]
        public void Construct(GameStateMachine.GameStateMachine gameStateMachine,
            ServiceProvider<GameLoopService> gameloopServiceProvider,GameSettings gameSettings, GameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameloopServiceProvider = gameloopServiceProvider;
            _gameFactory = gameFactory;
            _gameSettings = gameSettings;
            
            InitializeProvider();
            
        }
        
        public void Initialize(List<PlayerScore> playerScores)
        {
            if (!isServer)
                return;
            
            _playerScores = playerScores;
            foreach (var playerScore in playerScores)
            {
                playerScore.ScoreChanged += OnScoreChanged;
            }

            InitializePlayersWinnedRoundsMap();
        }

        private void InitializePlayersWinnedRoundsMap()
        {
            ServerManager serverManager = (ServerManager) NetworkManager.singleton;

            playersWinnedRounds = new Dictionary<NetworkConnectionToClient, int>();
            
            foreach (var conn in serverManager.ConnectionToClients)
            {
                playersWinnedRounds.Add(conn, 0);
            }
        }
        
        public void InitializeProvider()
        {
            _gameloopServiceProvider.Initialize(this);
        }

        private void OnScoreChanged(NetworkConnectionToClient connectionToClient, int score)
        {

            if (score >= _gameSettings.ScoreToWin)
            {
                StopBall();
                if (Round + 1 <= _gameSettings.RoundCount)
                {
                    AddRoundToPlayer(connectionToClient, 1);
                    RpcIncRound();
                    _roundWindow.AnimationEnded += OnRoundAnimationEnded;
                    RpcShowRoundWindowAnimation();
                    Debug.Log(Round);
                    return;
                }
                
                
                PlayerWin?.Invoke();
                var winnerConnection = GetWinnerConnection();
                ShowResult(winnerConnection);
            }

        }
        
        private NetworkConnectionToClient GetWinnerConnection()
        {
            var sortedMap = playersWinnedRounds
                .OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
            return sortedMap.Keys.Last();
        }
        private void AddRoundToPlayer(NetworkConnectionToClient conn, int value)
        {
            
            if (playersWinnedRounds.TryGetValue(conn, out int playerCurrentValue))
            {
                playersWinnedRounds[conn] = playerCurrentValue + value;
                return;
            }
            throw new Exception("NetworkConnectionToClient to add round is invalid");
        }
        
        private void ResetPlayerScore()
        {
            foreach (var score in _playerScores)
            {
                score.ResetScore();
            }
        }
        [ClientRpc]
        private void RpcIncRound()
        {
            Round++;
        }
        private void OnRoundAnimationEnded()
        {
            _roundWindow.AnimationEnded -= OnRoundAnimationEnded;
            
            ResetPlayerScore();
            _gameFactory.SpawnBall();
        }

        private void ShowResult(NetworkConnectionToClient conn)
        {
            RpcWin(conn);

            NetworkConnectionToClient loserConnection = GetLoserNetworkConnectionToClient(conn);
            
            RpcLose(loserConnection);

        }

        private NetworkConnectionToClient GetLoserNetworkConnectionToClient(NetworkConnectionToClient winnerConnection)
        {
            
            ServerManager serverManager = (ServerManager) NetworkManager.singleton;

            var loserConnection = serverManager.ConnectionToClients.First(connection =>
            {
                return connection != winnerConnection;
            });
            
            return loserConnection;
        }
        
        [ClientRpc]
        public void RpcShowRoundWindowAnimation()
        {
            _roundWindow.ShowAndCloseAnimation(Round);
        }
        
        [TargetRpc]
        public void RpcWin(NetworkConnection connection)
        {
            resultWindow.ShowResult(GameResult.Win);
        }

        [TargetRpc]
        public void RpcLose(NetworkConnectionToClient connection)
        {
            resultWindow.ShowResult(GameResult.Lose);
        }

        [ClientRpc]
        public void SubButt()
        {
            resultWindow.Clicked += OnClicked;
        }

        private void OnClicked()
        {
            _gameStateMachine.Enter<MenuState>();
        }

        public void StopGame()
        {
            StopPlayers();
            StopBall();
        }
        
        public void StartGame()
        {
            //StopPlayers();
            //StopBall();
        }

        private void InitializeGame()
        {
            
        }
        
        
        private void StopPlayers() =>
            _gameFactory.SpawnedPlayers.ForEach(player =>
            {
                player.GetComponent<PlayerMovement>().BlockInput();
            });

        public void StopBall()
        {
            _gameFactory.SpawnedBall.Stop();
            NetworkServer.Destroy(_gameFactory.SpawnedBall.gameObject);
        }
    }
}
