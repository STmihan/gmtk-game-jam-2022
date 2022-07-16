using Gameplay.Components.Enemy;
using Gameplay.Components.Share;
using Gameplay.Configs.Enemies;
using Leopotam.Ecs;

namespace Gameplay.Systems.Enemy.Setup
{
    public class EnemySetupSystem : IEcsRunSystem
    {
        private EnemyStatsConfig _config;
        private EcsFilter<EnemyTag, MovementComponent, RotationComponent>.Exclude<InitedTag> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var type = _filter.Get1(i).Type;
                var config = _config.GetConfigByType<Configs.Enemies.Stats.Enemy>(type);
                ref var movementComponent = ref _filter.Get2(i);
                ref var rotationComponent = ref _filter.Get3(i);

                movementComponent.Speed = config.MoveSpeed;
                rotationComponent.Duration = config.RotationDuration;
                _filter.GetEntity(i).Get<InitedTag>();
            }
        }
    }
}