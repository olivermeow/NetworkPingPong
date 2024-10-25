using Gameplay.Ball;
using Gameplay.Gate;
using Gameplay.Spawn;
using Gameplay.Views;
using Infarastructure.Services;
using UnityEngine;
using Zenject;

namespace Infarastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameLoopService gameLoopService;
        
        [SerializeField] private SpawnPointGroup _spawnPointGroup;
        [SerializeField] private ScoreViewGroup _viewGroup;
        [SerializeField] private GateGroup _gateGroup;
        [SerializeField] private Ball _ball;
        public override void InstallBindings()
        { 
            BindServices();
        }

        public void BindServices()
        {
            Container.Bind<GameLoopService>().FromInstance(gameLoopService).AsSingle().NonLazy();
            Container.Bind<GameFactory>().AsSingle().WithArguments(_spawnPointGroup, _viewGroup, _gateGroup, _ball).NonLazy();
        }

    }
}
