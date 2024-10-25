using System;

namespace Infarastructure.Services
{
    public class GameLoopServiceProvider
    {
        private GameLoopService _gameLoopService;
        
        public bool Initialized { get; private set; }
        
        public void Initialize(GameLoopService gameLoopService)
        {
            _gameLoopService = gameLoopService;
            Initialized = true;
        }

        public GameLoopService Get()
        {
            if (!Initialized)
                throw new ArgumentException("The provider has not been initialized");
            
            return _gameLoopService;
        }
    }
}