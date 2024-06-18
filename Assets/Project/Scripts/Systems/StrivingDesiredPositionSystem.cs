using Client;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class StrivingDesiredPositionSystem : IEcsRunSystem
{
    readonly EcsCustomInject<SceneContext> _sceneContext = default;
    private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

    public void Run(IEcsSystems systems)
    {
        if (_runtimeData.Value.GameState != GameState.PLAYING) return;

        foreach (var pickuped in _sceneContext.Value.PlayerView)
        {
            VelocityController(pickuped.ThisRigidbody);
        }

        VelocityController(_sceneContext.Value.PlayerView.PlayerRigidbody);
    }

    private void VelocityController(Rigidbody rigidbody)
    {
        if (rigidbody.velocity.y > 0f)
        {
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, _sceneContext.Value.PlayerView.VelocityClamp);
        }
    }
}
