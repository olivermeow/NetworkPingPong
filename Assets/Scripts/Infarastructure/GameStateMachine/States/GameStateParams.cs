

using System.Collections.Generic;
using Mirror;

namespace Infarastructure.GameStateMachine.States
{
    
    public struct GameStateParams
    {
        public List<NetworkConnectionToClient> Players;

        public GameStateParams(List<NetworkConnectionToClient> players)
        {
            Players = players;
        }
    }
}