using Infarastructure.GameStateMachine.States;
using UnityEngine;
using Zenject;

namespace Infrastucture.StateMachine
{
    public class StateResolverService
    {
        private DiContainer _container;

        public StateResolverService(DiContainer container)
        {
            _container = container;
        }
        
        public T GetState<T>() where T : class, IExitableState
        {
            return _container.Resolve<T>();
        }
    }
}