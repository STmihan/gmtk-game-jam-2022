using Gameplay.Components;
using Gameplay.Configs;
using Leopotam.Ecs;

namespace Gameplay.Systems
{
    public class PlayerRotationSetupSystem : IEcsInitSystem
    {
        private PlayerConfig _config;
        private EcsFilter<GameObjectComponent, PlayerTag> _filter;
        public void Init()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                ref var rotationComponent = ref entity.Get<RotationComponent>();
                rotationComponent.Speed = _config.PlayerMovementSpeed;
            }
        }
    }
}