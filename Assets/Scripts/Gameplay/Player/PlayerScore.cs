using System;
using Gameplay.Types;
using Mirror;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerScore : NetworkBehaviour
    {
        private int score;
    
        private Gate.Gate _gate;
        private NetworkConnectionToClient _clientConnection;

        public int Score => score;

        public event Action<NetworkConnectionToClient,int> ScoreChanged;
    
        public void Initialize(Gate.Gate gate, NetworkConnectionToClient clientConnection)
        {
            _gate = gate;
            _gate.Goooooal += OnGoal;
            _clientConnection = clientConnection;
        }
    
        public void AddScore()
        {
            score++;
            ScoreChanged?.Invoke(_clientConnection,score);
        }

        public void ResetScore()
        {
            score = 0;
            ScoreChanged?.Invoke(_clientConnection,score);
        }
        
        public void OnScoreChanged(int oldValue, int newValue)
        {
            ScoreChanged?.Invoke(_clientConnection, newValue);
            Debug.Log(123);
        }
        private void OnGoal(SideType obj)
        {
            AddScore();
        }
    }
}