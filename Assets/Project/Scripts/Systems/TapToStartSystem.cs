using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class TapToStartSystem : IEcsRunSystem 
    {
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;

        public void Run(IEcsSystems systems) 
        {
            if (_runtimeData.Value.GameState != GameState.NONE) return; // StartScreen.cs sets the state to NONE

            if (Input.GetMouseButtonUp(0))
            {
                _sceneContext.Value.PlayerView.PlayWarpEffect();
                systems.GetWorld().NewEntityRef<ChangeStateEvent>().NewGameState = GameState.PLAYING;
            }
        }
    }
}