using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class CameraFollowSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        private Vector3 cameraCurrentVelocity;

        public void Init(IEcsSystems systems)
        {
            Camera.main.transform.rotation = Quaternion.Euler(_staticData.Value.cameraRotation);
            Camera.main.transform.position = _runtimeData.Value.Pivot + _staticData.Value.cameraOffset;
        }

        public void Run(IEcsSystems systems)
        {
            Camera.main.transform.rotation = Quaternion.Euler(_staticData.Value.cameraRotation);
            Camera.main.transform.position = Vector3.SmoothDamp(
                                                Camera.main.transform.position, 
                                                _runtimeData.Value.Pivot + _staticData.Value.cameraOffset, 
                                                ref cameraCurrentVelocity, 
                                                _staticData.Value.cameraSmoothness);
        }
    }
}