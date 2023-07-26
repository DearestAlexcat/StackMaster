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
            VelocityController(pickuped);
        }

        VelocityController(_sceneContext.Value.PlayerView.PickupedPlayer);
    }

    private void VelocityController(Pickup pickuped)
    {
        if (pickuped.ThisRigidbody.velocity.y > 0f)
        {
            pickuped.ThisRigidbody.velocity = Vector3.ClampMagnitude(pickuped.ThisRigidbody.velocity, _sceneContext.Value.PlayerView.VelocityClamp);
        }
    }
}
