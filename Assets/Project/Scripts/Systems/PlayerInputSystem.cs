using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class PlayerInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        Vector3 m_StartingTouch;
        float pointerOffset, playerOffset;

        public void Init(IEcsSystems systems)
        {
            systems.GetWorld().NewEntity<PlayerInputComponent>();
        }

        public void Run(IEcsSystems systems) 
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            DoInput(systems);
        }

        float ExpDecay(float a, float b, float decay, float dt)
        {
            return b + (a - b) * Mathf.Exp(-decay * dt);
        }

        void DoInput(IEcsSystems systems)
        {
            float diffX = 0f;

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
                    diffX = (Input.mousePosition - m_StartingTouch).x;
                    diffX = diffX / UnityEngine.Screen.width;
                    m_StartingTouch = Input.mousePosition;
                }
            }

            var shift = diffX * _staticData.Value.pointerSpeed;
            pointerOffset = Mathf.Clamp(pointerOffset + shift, -_staticData.Value.border, _staticData.Value.border);

            playerOffset = ExpDecay(playerOffset, pointerOffset, _staticData.Value.playerSpeed, Time.deltaTime);

            systems.GetWorld().NewEntityRef<PlayerInputComponent>().OffsetX = playerOffset;
        }
    }
}