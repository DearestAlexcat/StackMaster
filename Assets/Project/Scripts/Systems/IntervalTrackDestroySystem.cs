using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class IntervalTrackDestroySystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsCustomInject<TrackViewService> _tracksView = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;

        private float cooldown;

        public void Init(IEcsSystems systems)
        {
            cooldown = _staticData.Value.intervalToTrackDestroy;
        }

        public void Run(IEcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;
          
            cooldown -= Time.deltaTime;

            if (cooldown <= 0f)
            {
                _tracksView.Value.RemoveFirst();
                cooldown = _staticData.Value.intervalToTrackDestroy;
            }
        }
    }
}