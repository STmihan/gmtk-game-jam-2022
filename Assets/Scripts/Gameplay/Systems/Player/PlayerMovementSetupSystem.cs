using Gameplay.Components;
using Gameplay.Configs;
using Leopotam.Ecs;

namespace Gameplay.Systems
{
    public class PlayerMovementSetupSystem : IEcsInitSystem
    {
        private EcsFilter<PlayerTag, GameObjectComponent> _filter;
        public void Init()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                entity.Get<MovementComponent>();
            }
        }
    }
}