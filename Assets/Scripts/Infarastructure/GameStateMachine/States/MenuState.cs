using UnityEngine;

namespace Infarastructure.GameStateMachine.States
{
    public class MenuState : IState
    {
        private const string SceneName = "Menu";
        private GameStateMachine _gameStateMachine;

        public MenuState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        public void Exit()
        {

        }

        public void Enter()
        { 
            Debug.Log("menustate");
            
        }
    }
}
