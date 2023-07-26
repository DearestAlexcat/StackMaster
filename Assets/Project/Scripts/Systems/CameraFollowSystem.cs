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
            SetOrientation();
        }

        private void SetOrientation()
        {
            Vector3 currentPosition = Camera.main.transform.position;
            Vector3 targetPoint = _runtimeData.Value.Pivot + _staticData.Value.cameraOffset;

            Camera.main.transform.rotation = Quaternion.Euler(_staticData.Value.cameraRotation);
            Camera.main.transform.position = Vector3.SmoothDamp(currentPosition, targetPoint, ref cameraCurrentVelocity, _staticData.Value.cameraSmoothness);
        }
    }
}