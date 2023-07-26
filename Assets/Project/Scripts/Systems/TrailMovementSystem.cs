using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class TrailMovementSystem : IEcsRunSystem 
    {
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Run(IEcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            var distance = _sceneContext.Value.LevelRoot.forward * Time.deltaTime * _staticData.Value.playerVerticalSpeed;

            if (_sceneContext.Value.PlayerView.Trail.positionCount > 0)
            {
                int count = _sceneContext.Value.PlayerView.Trail.positionCount;
                var trail = _sceneContext.Value.PlayerView.Trail;

                Vector3 position;

                for (int i = 0; i < count; i++)
                {
                    position = trail.GetPosition(i);
                    trail.SetPosition(i, position - distance);
                }
            }
        }
    }
}