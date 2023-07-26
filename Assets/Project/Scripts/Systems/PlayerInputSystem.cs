using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class PlayerInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsFilterInject<Inc<PlayerInputComponent>> _inputFilter = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        Vector3 m_StartingTouch;
        float offsetX, offsetX2;

        public void Init(IEcsSystems systems)
        {
            systems.GetWorld().NewEntity<PlayerInputComponent>();
        }

        public void Run(IEcsSystems systems) 
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            DoInput();
        }

        public void DoInput()
        {
            Vector2 diff = Vector3.zero;

            if (Input.GetMouseButtonUp(0))
            {
                m_StartingTouch = Vector2.zero;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                m_StartingTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                if (Vector3.Distance(m_StartingTouch, Input.mousePosition) > _staticData.Value.deadZone)
                {
                    diff = Input.mousePosition - m_StartingTouch;
                    diff = new Vector2(diff.x / UnityEngine.Screen.width, diff.y / UnityEngine.Screen.height);
                    m_StartingTouch = Input.mousePosition;
                }
            }

            if (offsetX + diff.x * _staticData.Value.playerSpeed < _staticData.Value.border && 
                offsetX + diff.x * _staticData.Value.playerSpeed > -_staticData.Value.border)
            {
                offsetX += diff.x * _staticData.Value.playerSpeed; 
            }
            else
            {
                offsetX = Mathf.Lerp(offsetX, Mathf.Sign(offsetX) * _staticData.Value.border, Time.deltaTime * 2);
            }

            offsetX2 = Mathf.Lerp(offsetX2, offsetX, 0.1f);

            foreach (var entity in _inputFilter.Value)
            {
                _inputFilter.Pools.Inc1.Get(entity).OffsetX = offsetX2;
            }
        }
    }
}