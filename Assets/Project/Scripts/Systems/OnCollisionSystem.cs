using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class OnCollisionSystem : IEcsInitSystem, IEcsRunSystem
    {
        EcsFilter _filterEnter;
        EcsPool<OnCollisionEnterEvent> _poolEnter;

        private readonly EcsCustomInject<SceneContext> _sceneContext = default;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filterEnter = world.Filter<OnCollisionEnterEvent>().End();
            _poolEnter = world.GetPool<OnCollisionEnterEvent>();
        }

        public void Run(IEcsSystems systems) 
        {
            foreach (var entity in _filterEnter)
            {
                ref var eventData = ref _poolEnter.Get(entity);
                var collider = eventData.collider;
               
                if (collider.CompareTag("Pickuped"))
                {
                    var pickup = collider.gameObject.GetComponent<Pickup>();

                    if (_sceneContext.Value.PlayerView.ContainsPickuped(pickup))
                    {
                        pickup.transform.parent.SetParent(eventData.senderGameObject.transform);
                        pickup.ThisRigidbody.isKinematic = true;

                        _sceneContext.Value.PlayerView.RemovePickuped(pickup);
                        _sceneContext.Value.PlayerView.CheckLevelComplete();
                    }
                }

                if(collider.CompareTag("Player"))
                {
                    _sceneContext.Value.PlayerView.SetLevelLoseState();
                }
            }
        }
    }
}