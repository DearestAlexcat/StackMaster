using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class CameraShakeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CameraShakeReguest>> _shakeFilter = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;

        public void Run(IEcsSystems systems)
        {
            if (_shakeFilter.Value.IsEmpty()) return;

            Shake().Forget();
        }

        async UniTask Shake()
        {
            var origPosition = Camera.main.transform.localPosition;
            float elapsedTime = 0f;
            var thisTransform = Camera.main.transform;

            while (elapsedTime < _staticData.Value.shakeTime)
            {
                var randomPoint = origPosition + Random.insideUnitSphere * _staticData.Value.shakeAmount;
                
                thisTransform.localPosition = ExpDecay(thisTransform.localPosition, randomPoint, _staticData.Value.shakeSpeed, Time.deltaTime);

                await UniTask.NextFrame();

                elapsedTime += Time.deltaTime;
            }

            thisTransform.localPosition = origPosition;
        }

        Vector3 ExpDecay(Vector3 a, Vector3 b, float decay, float dt)
        {
            return b + (a - b) * Mathf.Exp(-decay * dt);
        }
    }
}
