using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class FxSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CubeStackFXRequest>> _stackFilter = null;

        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;

        public void Run(IEcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var it in _stackFilter.Value)
            {
                var fx = Object.Instantiate(_staticData.Value.CubeStackEffect, _stackFilter.Pools.Inc1.Get(it).Position, Quaternion.identity, _sceneContext.Value.LevelRoot);
                Object.Destroy(fx, 10f);
            }
        }
    }
}
