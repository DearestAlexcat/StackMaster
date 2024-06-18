using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    class ChangeStateSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsFilterInject<Inc<ChangeStateEvent>> _stateFilter = default;
        private readonly EcsWorldInject _world = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _stateFilter.Value)
            {
                var state = _stateFilter.Pools.Inc1.Get(entity).NewGameState;

                _runtimeData.Value.GameState = state;
                      
                switch (state)
                {
                    case GameState.NONE:
                        break;
                    case GameState.BEFORE:
                        Service<UI>.Get().StartScreen.Show();
                        break;
                    case GameState.PLAYING:
                        Service<UI>.Get().StartScreen.Show(false);
                        break;
                    case GameState.LOSE:
                        Service<UI>.Get().LoseScreen.Show();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _world.Value.DelEntity(entity);
            }
        }
    }
}
