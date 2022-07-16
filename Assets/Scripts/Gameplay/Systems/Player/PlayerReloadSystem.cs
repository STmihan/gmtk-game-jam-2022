using System.Linq;
using Gameplay.Components;
using Gameplay.Configs.Enemies;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player
{
    public class PlayerReloadSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, TryReloadComponent> _tryReloadFilter = null;
        public void Run()
        {
            foreach(var i in _tryReloadFilter)
            {
                ref var entity = ref _tryReloadFilter.GetEntity(i);
                entity.Get<TryReloadComponent>();
            }
        }
    }
}
