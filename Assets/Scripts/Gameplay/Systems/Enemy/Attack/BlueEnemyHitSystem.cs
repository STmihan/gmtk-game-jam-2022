using System.Collections.Generic;
using System.Linq;
using Gameplay.Components.Camera;
using Gameplay.Components.Enemy;
using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Components.Share.Attack;
using Gameplay.Configs.Attacks;
using Gameplay.Configs.Enemies;
using Gameplay.Configs.Enemies.Stats;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Enemy.Attack
{
    public class BlueEnemyHitSystem : IEcsRunSystem
    {
        private EcsFilter<HitEvent> _filter;
        private EcsFilter<PlayerTag, CharacterViewComponent, HpComponent> _playerFilter;
        private AttackConfig _config;
        private EnemyStatsConfig _statsConfig;
        private EcsWorld _world;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                var hit = _filter.Get1(i);
                var type = hit.Type;
                if(type != typeof(EnemyBlueAttack)) continue;
                var config = (EnemyBlueAttack)_config.Attacks.FirstOrDefault(attack => attack.Type == type);
                var statsConfig = _statsConfig.GetConfigByType<RangedProjectileEnemy>(typeof(RangedProjectileEnemy));
                if (config == null) continue;
                var vfxPrefab = config.HitVFX;
                Object.Instantiate(vfxPrefab, hit.Position, Quaternion.identity);
                ref var hpComponent = ref _playerFilter.Get3(i);
                hpComponent.Hp -= statsConfig.Damage;
                _world.NewEntity().Get<CameraShakeComponent>();
                _filter.GetEntity(i).Del<HitEvent>();
            }
        }
    }
}