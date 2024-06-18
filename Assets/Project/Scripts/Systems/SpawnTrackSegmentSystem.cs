using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using DG.Tweening;
using Game;

namespace Client 
{
    sealed class SpawnTrackSegmentSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsCustomInject<TrackViewService> _tracksView = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<RandomService> _randomService = default;

        private readonly EcsFilterInject<Inc<SpawnTrackSegmentRequest>> _spawnRequestFilter = default;

        public void Init(IEcsSystems systems)
        {
            TrackInitialize(systems);
        }

        public void Run(IEcsSystems systems) 
        {
            foreach (var item in _spawnRequestFilter.Value)
            {
                SpawnTrack(item);
            }
        }

        private void TrackInitialize(IEcsSystems systems)
        {
            var firstTrack = GameObject.FindGameObjectWithTag("TrackGround").GetComponent<TrackView>();
            _tracksView.Value.AddLast(firstTrack);

            ref var component = ref systems.GetWorld().NewEntityRef<SpawnTrackSegmentRequest>();
            component.Number = 3;
            component.IsUpper = true;
        }

        private void SpawnTrack(int entity)
        {
            ref var component = ref _spawnRequestFilter.Pools.Inc1.Get(entity);

            for (int i = 0; i < component.Number; i++)
            {
                var trackSegment = Object.Instantiate(GetRandomTrack(), Vector3.zero, Quaternion.identity, _sceneContext.Value.LevelRoot);

                SetPosition(trackSegment);

                if(!component.IsUpper)
                {
                    SetObjectAppearance(trackSegment);
                }

                _tracksView.Value.AddLast(trackSegment);
            }
        }

        private TrackView GetRandomTrack()
        {
            var tracks = _staticData.Value.TrackViews;
            return tracks[_randomService.Value.Range(0, tracks.Count)];
        }

        private void SetObjectAppearance(TrackView trackSegment)
        {
            var temp = trackSegment.transform.position;
            temp.y = -100;
            trackSegment.transform.position = temp;

            trackSegment.transform
                .DOMoveY(0f, _staticData.Value.spawnSpeed)
                .SetEase(_staticData.Value.spawnEase)
             // .SetSpeedBased()
                .SetLink(trackSegment.gameObject);
        }

        private void SetPosition(TrackView trackSegment)
        {
            trackSegment.transform.forward = _tracksView.Value.Last().transform.forward;
            trackSegment.transform.position = _tracksView.Value.Last().ExitPoint.position +
                                            (trackSegment.transform.position - trackSegment.EntryPoint.position);
        }
    }
}