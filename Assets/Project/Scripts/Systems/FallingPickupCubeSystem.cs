using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class FallingPickupCubeSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<FallingRequest>> _fallingFilter = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;

        public void Run (IEcsSystems systems) 
        {
            if (_fallingFilter.Value.IsEmpty()) return;

            _sceneContext.Value.PlayerView.ApplyForceToFall();
        }
    }
}