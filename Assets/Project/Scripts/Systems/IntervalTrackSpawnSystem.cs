using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class IntervalTrackSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;
        
        private float cooldown;

        public void Init(IEcsSystems systems)
        {
            cooldown = _staticData.Value.intervalToTrackSpawn;
        }

        public void Run(IEcsSystems systems) 
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            cooldown -= Time.deltaTime;

            if (cooldown <= 0f)
            {
                systems.GetWorld().NewEntityRef<SpawnTrackSegmentRequest>().Number = 1;
                cooldown = _staticData.Value.intervalToTrackSpawn;
            }
        }
    }
}