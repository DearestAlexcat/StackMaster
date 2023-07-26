using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class PopUpSystem : IEcsRunSystem 
    {
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsFilterInject<Inc<PopUpRequest>> _popUpFilter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var it in _popUpFilter.Value)
            {
                var c = _popUpFilter.Pools.Inc1.Get(it);
                var popUP = Object.Instantiate<PopUpText>(_staticData.Value.PlusOne, c.SpawnPosition, c.SpawnRotation, c.Parent);
                popUP.textUP.text = c.TextUP;

                _popUpFilter.Pools.Inc1.Del(it);
            }
        }
    }
}