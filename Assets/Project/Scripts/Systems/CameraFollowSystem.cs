using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class CameraFollowSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        //private readonly EcsCustomInject<SceneContext> _sceneContext = default;

        private Vector3 cameraCurrentVelocity;
        
        //private float yOffsetBase;
        //private int max = 8;
        //private int min = 4;
        //private int index = 0;

        public void Init(IEcsSystems systems)
        {
            //yOffsetBase = _staticData.Value.cameraOffset.y;
            Camera.main.transform.rotation = Quaternion.Euler(_staticData.Value.cameraRotation);
            Camera.main.transform.position = _runtimeData.Value.Pivot + _staticData.Value.cameraOffset;
        }

        public void Run(IEcsSystems systems)
        {
            //PlayerPositionMonitoring();
            SetOrientation();
        }

        private void SetOrientation()
        {
            Vector3 currentPosition = Camera.main.transform.position;
            Vector3 targetPoint = _runtimeData.Value.Pivot + _staticData.Value.cameraOffset;

            Camera.main.transform.rotation = Quaternion.Euler(_staticData.Value.cameraRotation);
            Camera.main.transform.position = Vector3.SmoothDamp(currentPosition, targetPoint, ref cameraCurrentVelocity, _staticData.Value.cameraSmoothness);
        }

        //private void PlayerPositionMonitoring()
        //{
        //    Vector3 offset = _staticData.Value.cameraOffset;
        //    var array = _sceneContext.Value.PlayerView.Pickuped;
 
        //    if(array.Count >= max * (index + 1))
        //    {
        //        index += array.Count / max;
        //        offset.y = yOffsetBase + max * index;
        //        _staticData.Value.cameraOffset = offset;
        //    }

        //    if (array.Count <= min * index)
        //    {
        //        index -= array.Count / max;
        //        offset.y = yOffsetBase - max * index;
        //        _staticData.Value.cameraOffset = offset;
        //    }
        //}
    }
}