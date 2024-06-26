using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using LeoEcsPhysics;
using System.Collections;
using UnityEngine;

namespace Client 
{
    sealed class EcsStartup : MonoBehaviour {
        
        private EcsWorld _world;

        IEcsSystems _update;
        IEcsSystems _fixedUpdate;

        public RuntimeData runtimeData;
        public StaticData staticData;
        public SceneContext sceneContext;

        [SerializeField] private bool _useSeed = default;
        [SerializeField] private int _randomSeed = default;

        public IEnumerator Start() 
        {
            Application.targetFrameRate = 60;

            _world = new EcsWorld();
            _update = new EcsSystems(_world);
            _fixedUpdate = new EcsSystems(_world);
            EcsPhysicsEvents.ecsWorld = _world;

            runtimeData = new RuntimeData();

            Service<EcsWorld>.Set(_world);

            _update
                .Add(new InitializeSystem())
                .Add(new ChangeStateSystem())

                .Add(new PlayerInputSystem())
                .Add(new PlayerMovementSystem())
                .DelHere<PlayerInputComponent>()

                .Add(new CameraShakeSystem())
                .DelHere<CameraShakeReguest>()
                .Add(new CameraFollowSystem())

                .Add(new IntervalTrackSpawnSystem())
                .Add(new IntervalTrackDestroySystem())
                .Add(new SpawnTrackSegmentSystem())
                .DelHere<SpawnTrackSegmentRequest>()
                .Add(new TrackSegmantMovementSystem())
                .Add(new TrailMovementSystem())

                .Add(new PopUpSystem())
                .Add(new FxSystem())
                .DelHere<CubeStackFXRequest>()

                .Add(new FallingPickupCubeSystem())
                .DelHere<FallingRequest>()

                .Add(new TapToStartSystem())

                .Add(new PlayerFallSystem())

#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject(staticData, runtimeData, sceneContext, GetComponent<TrackViewService>())
                .Inject(new RandomService(_useSeed ? _randomSeed : null))
                .Init();

            _fixedUpdate
                .Add(new OnTriggerSystem())
                .Add(new OnCollisionSystem())
                .Add(new StrivingDesiredPositionSystem())
                .Inject(sceneContext, runtimeData)
                .DelHerePhysics()
                .Init();

            yield return null;
        }

        public void Update() 
        {
            _update?.Run();
        }

        public void FixedUpdate()
        {
            _fixedUpdate?.Run();
        }

        public void OnDestroy() 
        {
            if (_world != null)
            {
                EcsPhysicsEvents.ecsWorld = null;
                _world.Destroy();
                _world = null;
            }

            if (_update != null) 
            {
                _update.Destroy();   
                _update = null;
            }

            if (_fixedUpdate != null)
            {              
                _fixedUpdate.Destroy();            
                _fixedUpdate = null;            
            }
        }
    }
}