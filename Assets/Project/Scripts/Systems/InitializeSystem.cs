using Leopotam.EcsLite;

namespace Client
{
    class InitializeSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            Service<UI>.Get().CloseAll();
            systems.GetWorld().ChangeState(GameState.BEFORE);
        }
    }
}