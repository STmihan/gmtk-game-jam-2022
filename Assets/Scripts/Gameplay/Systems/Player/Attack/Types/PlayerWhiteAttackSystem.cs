using System.Collections.Generic;
using System.Linq;
using Gameplay.Components.Enemy;
using Gameplay.Components.Share;
using Gameplay.Components.Share.Attack;
using Gameplay.Configs.Attacks;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player.Attack.Types
{
    public class PlayerWhiteAttackSystem : IEcsRunSystem
    {
        private EcsFilter<HitEvent> _filter;
        private EcsFilter<EnemyTag, CharacterViewComponent, HpComponent> _enemiesFilter;
        private AttackConfig _config;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                var hit = _filter.Get1(i);
                var type = hit.Type;
                if(type != typeof(PlayerWhiteAttack)) return;
                var config = (PlayerWhiteAttack)_config.Attacks.FirstOrDefault(attack => attack.Type == type);
                if (config == null) continue;
                var vfxPrefab = config.HitVFX;
                Object.Instantiate(vfxPrefab, hit.Position, Quaternion.identity);
                var closeTransform = GetCloseTransform(hit.Position);
                var enemyEntity = GetEnemyByTransform(closeTransform);
                ref var hpComponent = ref enemyEntity.Get<HpComponent>();
                hpComponent.Hp -= config.Damage;
                _filter.GetEntity(i).Del<HitEvent>();
            }
        }

        private EcsEntity GetEnemyByTransform(Transform transform)
        {
            foreach (var i in _enemiesFilter)
                if (_enemiesFilter.Get2(i).View.transform == transform)
                    return _enemiesFilter.GetEntity(i);
            return default;
        }
        
        private Transform GetCloseTransform(Vector3 target)
        {
            var enemies = new List<Transform>();
            foreach (var e in _enemiesFilter)
                enemies.Add(_enemiesFilter.Get2(e).View.CharacterController.transform);
            var result = enemies[0];
            foreach (var enemy in enemies.Where(enemy =>
                         Vector3.Distance(enemy.position, target) <
                         Vector3.Distance(result.position, target)))
                result = enemy;

            return result;
        }
    }
}