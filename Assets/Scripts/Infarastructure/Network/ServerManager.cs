using System;
using System.Collections.Generic;
using Infarastructure.Services;
using Mirror;
using UnityEngine;
using Zenject;

namespace Infarastructure.Network
{
    public class ServerManager : NetworkManager
    {
        private List<NetworkConnectionToClient> _clientsConnections = new List<NetworkConnectionToClient>();
        public List<NetworkConnectionToClient> ConnectionToClients => _clientsConnections;
        
        public event Action<List<NetworkConnectionToClient>> AllPlayersConected;
        
        public event Action<NetworkConnectionToClient> PlayerConnected;

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            _clientsConnections.Add(conn);

            PlayerConnected?.Invoke(conn);
            
            if (_clientsConnections.Count == maxConnections)
            {
                AllPlayersConected?.Invoke(_clientsConnections);
            }
        }

        public override void OnClientDisconnect()
        {
            base.OnClientDisconnect();
            Debug.Log("CLIENT DISCONNECT");
        }

        public override void OnStopHost()
        {
            _clientsConnections.Clear();
            Debug.Log("HOST DISCONNECT");
            base.OnStopHost();
        }
    }
}