using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class PlayerFallSystem : IEcsRunSystem 
    {
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Run(IEcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.LOSE) return;
            
            FallMonitoring();
        }

        private void FallMonitoring()
        {
            if (_sceneContext.Value.PlayerView.RagdollHolder.position.y < _staticData.Value.lowerLimitByY)
            {
                _sceneContext.Value.PlayerView.PlayerObject.gameObject.SetActive(false);
            }
        }
    }
}