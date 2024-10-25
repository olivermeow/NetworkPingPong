namespace Infarastructure.GameStateMachine.States
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}