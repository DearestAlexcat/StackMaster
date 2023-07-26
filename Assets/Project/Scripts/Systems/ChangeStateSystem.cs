using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Cysharp.Threading.Tasks;

namespace Client
{
    class ChangeStateSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsCustomInject<UI> _ui = default;

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
                        _ui.Value.StartScreen.Show();
                        break;
                    case GameState.PLAYING:
                        _ui.Value.StartScreen.Show(false);
                        break;
                    case GameState.LOSE:
                        _ui.Value.LoseScreen.Show();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _world.Value.DelEntity(entity);
            }
        }
    }
}
