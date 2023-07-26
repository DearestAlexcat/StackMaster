using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class OnTriggerSystem : IEcsInitSystem, IEcsRunSystem
    {
        EcsFilter _filterEnter;
        EcsPool<OnTriggerEnterEvent> _poolEnter;
        EcsFilter _filterExit;
        EcsPool<OnTriggerExitEvent> _poolExit;

        private readonly EcsCustomInject<SceneContext> _sceneContext = default;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filterEnter = world.Filter<OnTriggerEnterEvent>().End();
            _poolEnter = world.GetPool<OnTriggerEnterEvent>();
            _filterExit = world.Filter<OnTriggerExitEvent>().End();
            _poolExit = world.GetPool<OnTriggerExitEvent>();
        }

        public void Run(IEcsSystems systems) 
        {
            foreach (var entity in _filterEnter)
            {
                ref var eventData = ref _poolEnter.Get(entity);
                var other = eventData.collider;

                EcsWorld world = systems.GetWorld();

                if (other.CompareTag("Pickup"))
                {
                    var pickupCube = other.gameObject.GetComponent<Pickup>();
                    var player = _sceneContext.Value.PlayerView;
                    var position = player.GetNextDesiredPosition();

                    CreateCubeStackFX(world, position);
                    CreatePopUpText(world, position, pickupCube.Parent.parent);

                    player.AddedPickupCube(pickupCube);
                    player.UpdatePickupedPlayerPosition();
                    player.PlayJumpAnimation();
                }
 
                if (other.CompareTag("WallTrigger"))
                {
                    world.NewEntity<CameraShakeReguest>();
                }     
            }

            foreach (var entity in _filterExit)
            {
                ref var eventData = ref _poolExit.Get(entity);
                var other = eventData.collider;

                if (other.CompareTag("WallTrigger"))
                {
                    systems.GetWorld().NewEntity<FallingRequest>();
                }
            }
        }

        private void CreatePopUpText(EcsWorld world, Vector3 position, Transform parent)
        {
            ref var component = ref world.NewEntityRef<PopUpRequest>();
            component.SpawnPosition = position;
            component.SpawnRotation = Quaternion.identity;
            component.TextUP = "+1";
            component.Parent = parent;
        }

        private void CreateCubeStackFX(EcsWorld world, Vector3 position)
        {
            world.NewEntityRef<CubeStackFXRequest>().Position = position;
        }
    }
}