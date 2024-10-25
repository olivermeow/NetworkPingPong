using System.Collections.Generic;
using Infarastructure.Network;
using Infarastructure.Services;
using Infrastucture.StateMachine;
using Mirror;
using UnityEngine;

namespace Infarastructure.GameStateMachine.States
{
    public class WaitingPlayersState : IState
    {
        
        
        private readonly GameStateMachine _gameStateMachine;
        private ServerManager _serverManager;
        private WaitingPlayerPanel _waitingPlayerPanel;

        public WaitingPlayersState(GameStateMachine gameStateMachine, WaitingPlayerPanel waitingPlayerPanel)
        {
            _gameStateMachine = gameStateMachine;
            _waitingPlayerPanel = waitingPlayerPanel;
        }
        public void Exit()
        {
            _serverManager.AllPlayersConected -= OnAllPlayersConnected;
            
            _waitingPlayerPanel.Hide();
        }
        public void Enter()
        {
            _waitingPlayerPanel.Show();
            var serverManager = (ServerManager)NetworkManager.singleton;
            _serverManager = serverManager;
            _serverManager.AllPlayersConected += OnAllPlayersConnected;
            
            if (NetworkManager.singleton.maxConnections == 1)
            {
                serverManager.PlayerConnected += client =>
                {
                    _gameStateMachine.Enter<GameLoopState,GameStateParams>(new GameStateParams(new List<NetworkConnectionToClient> {client})
                    );
                };
               
            }
        }

        private void OnAllPlayersConnected(List<NetworkConnectionToClient> networkConnectionsToClients)
        {
            _gameStateMachine.Enter<GameLoopState,GameStateParams>(new GameStateParams(networkConnectionsToClients));
        }
        
    }
}