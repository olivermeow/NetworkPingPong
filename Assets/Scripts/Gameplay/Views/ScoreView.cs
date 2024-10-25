using Gameplay.Player;
using Gameplay.Types;
using Mirror;
using TMPro;
using UnityEngine;

namespace Gameplay.Views
{
    public class ScoreView : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        [field: SerializeField] public SideType SideType { get; private set; }
    
        private PlayerScore _playerScore;
    
        public void Initialize(PlayerScore playerScore)
        {
            _playerScore = playerScore;
            playerScore.ScoreChanged += OnScoreChanged;
        }

        private void OnScoreChanged(NetworkConnectionToClient client, int obj)
        {
            Render(obj);
        }
    
        [ClientRpc]
        private void Render(int value)
        {
            scoreText.text = value.ToString();
        }
    }
}