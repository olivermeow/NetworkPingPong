using Mirror;

namespace Infarastructure.GameStateMachine.States
{
    public class Player
    {
        public Player(NetworkConnectionToClient networkConnectionToClient)
        {
            NetworkConnectionToClient = networkConnectionToClient;
            //Name = name;
        }

        //public string Name { get; }
        public NetworkConnectionToClient NetworkConnectionToClient { get; }
    }
}