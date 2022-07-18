using Gameplay.Components;
using Gameplay.Components.Share;
using Gameplay.Configs.Enemies;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Share
{
    public class TimeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EnemySpawnConfig _enemySpawnConfig;
        private EcsWorld _world;
        private EcsEntity _entity;

        public void Init()
        {
            _entity = _world.NewEntity();
            ref var timeComponent = ref _entity.Get<TimeComponent>();
            timeComponent.Time = 0;
        }

        public void Run()
        {
            ref var timeComponent = ref _entity.Get<TimeComponent>();
            timeComponent.Time += Time.deltaTime;
            timeComponent.TimeInPercent = GetCurrentTimeInPercent();

        }
        
        private float GetCurrentTimeInPercent()
        {
            ref var timeComponent = ref _entity.Get<TimeComponent>();
            return timeComponent.Time / (_enemySpawnConfig.MaxTimeInMinutes * 60);
        }
    }
}