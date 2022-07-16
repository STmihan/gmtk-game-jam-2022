using Gameplay.Components;
using Gameplay.Configs;
using Leopotam.Ecs;

namespace Gameplay.Systems
{
    public class PlayerRotationSetupSystem : IEcsInitSystem
    {
        private PlayerConfig _config;
        private EcsFilter<PlayerTag, GameObjectComponent> _filter;
        public void Init()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                entity.Get<RotationComponent>();
            }
        }
    }
}