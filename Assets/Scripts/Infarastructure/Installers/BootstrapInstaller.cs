using Infarastructure.GameStateMachine.States;
using Infarastructure.Services;
using Infarastructure.StaticData;
using Infrastucture.StateMachine;
using UnityEngine;
using Zenject;

namespace Infarastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameObject waitingPlayersPanel;
        [SerializeField] private GameSettings gameSettings;

        public override void InstallBindings()
        {
            BindGameSettings();
            BindServices();
            BindStates();
            BindGSM();
        }

        public void BindGSM()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine.GameStateMachine>().AsSingle().NonLazy();
        }
        public void BindServices()
        {
            Container.Bind<WaitingPlayerPanel>().FromComponentInNewPrefab(waitingPlayersPanel)
                .WithGameObjectName("Waiting Players Panel")
                .UnderTransformGroup("Infrastructure")
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<StateResolverService>().AsSingle().NonLazy();
            Container.Bind<GameLoopServiceProvider>().AsSingle().NonLazy();
            
            Container.Bind<ServiceProvider<GameLoopService>>().AsSingle().NonLazy();
            Container.Bind<ServiceProvider<GameFactory>>().AsSingle().NonLazy();
        }
        public void BindStates()
        {
            Container.Bind<BootstrapState>().AsSingle().NonLazy();
            Container.Bind<MenuState>().AsSingle().NonLazy();
            Container.Bind<GameLoopState>().AsSingle().NonLazy();
            Container.Bind<WaitingPlayersState>().AsSingle().NonLazy();
        }

        public void BindGameSettings()
        {
            Container.Bind<GameSettings>().FromInstance(gameSettings).AsSingle().NonLazy();
        }
    }
}