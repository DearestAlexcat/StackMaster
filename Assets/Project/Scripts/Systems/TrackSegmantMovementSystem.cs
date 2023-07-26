using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class TrackSegmantMovementSystem : IEcsRunSystem 
    {
        private readonly EcsCustomInject<TrackViewService> _tracksView = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Run(IEcsSystems systems) 
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            var distance = _sceneContext.Value.LevelRoot.forward * Time.deltaTime * _staticData.Value.playerVerticalSpeed;

            foreach (var item in _tracksView.Value)
            {
                item.transform.position -= distance;
            }            
        }
    }
}