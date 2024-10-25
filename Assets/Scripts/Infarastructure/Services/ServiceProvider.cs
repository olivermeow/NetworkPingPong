using System;

namespace Infarastructure.Services
{
    public class ServiceProvider<T> where T : class

    {
        private T _gameloopService;

        public bool Initialized { get; private set; }

        public void Initialize(T gameloopService)
        {
            _gameloopService = gameloopService;
            Initialized = true;
        }

        public T Get()
        {
            if (!Initialized)
                throw new ArgumentException($"The {nameof(T)}provider has not been initialized");

            return _gameloopService;
        }
    }
}