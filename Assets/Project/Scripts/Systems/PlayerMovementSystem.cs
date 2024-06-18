using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class PlayerMovementSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        private readonly EcsFilterInject<Inc<PlayerInputComponent>> _inputFilter = default;

        public void Init(IEcsSystems systems)
        {
            _runtimeData.Value.Pivot = _sceneContext.Value.PlayerView.transform.position;
        }

        public void Run(IEcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            PlayerMovement();
        }

        private void PlayerMovement()
        {
            foreach (var entity in _inputFilter.Value)
            {
                var offsetX = _inputFilter.Pools.Inc1.Get(entity).OffsetX;
                Vector3 newPosition = _runtimeData.Value.Pivot + _sceneContext.Value.LevelRoot.right * offsetX;
                _sceneContext.Value.PlayerView.transform.position = newPosition;
            }
        }
    }
}