using Infrastucture.StateMachine;
using UnityEngine;

namespace Infarastructure.GameStateMachine.States
{
    public class BootstrapState : IState
    {
        private GameStateMachine _gameStateMachine;


        public BootstrapState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        public void Exit()
        {
            
        }
        public void Enter()
        {
            Debug.Log("BootstrapState");
            _gameStateMachine.Enter<MenuState>();
        }
    }
}
