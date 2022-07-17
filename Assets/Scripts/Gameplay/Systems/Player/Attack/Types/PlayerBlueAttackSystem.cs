using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Gameplay.Components.Enemy;
using Gameplay.Components.Share;
using Gameplay.Components.Share.Attack;
using Gameplay.Configs.Attacks;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player.Attack.Types
{
    public class PlayerBlueAttackSystem : IEcsRunSystem
    {
        private EcsFilter<HitEvent> _firestHitFilter;
        private EcsFilter<EnemyTag, CharacterViewComponent> _enemiesFilter;
        private AttackConfig _config;

        public void Run()
        {
            foreach (var i in _firestHitFilter)
            {
                var hit = _firestHitFilter.Get1(i);
                var type = hit.Type;
                if (type != typeof(PlayerBlueAttack)) continue;
                var config = (PlayerBlueAttack)_config.Attacks.FirstOrDefault(attack => attack.Type == type);
                if (config == null) continue;
                var trail = config.Trail;
                var explosion = config.Explosion;
                var chainCount = config.ChainCount;
                var target = GetCloseTransform(hit.Position);
                if (target != null)
                {
                    var chains = GetChain(target, chainCount);
                    var sequence = DOTween.Sequence();
                    var view = Object.Instantiate(trail, hit.Position, Quaternion.identity);
                    foreach (var chain in chains)
                    {
                        sequence.Append(view.transform.DOMove(chain.position, 0.1f).OnComplete(() =>
                        {
                            Object.Instantiate(explosion, chain.position, Quaternion.identity);
                            var enemyEntity = GetEnemyByTransform(chain);
                            ref var hpComponent = ref enemyEntity.Get<HpComponent>();
                            hpComponent.Hp -= config.Damage;
                        }));
                    }
                }

                _firestHitFilter.GetEntity(i).Del<HitEvent>();
            }
        }
        
        private EcsEntity GetEnemyByTransform(Transform transform)
        {
            foreach (var i in _enemiesFilter)
                if (_enemiesFilter.Get2(i).View.transform == transform)
                    return _enemiesFilter.GetEntity(i);
            return default;
        }

        private Transform[] GetChain(Transform firstTarget, int chainCount)
        {
            var result = new List<Transform>
            {
                firstTarget
            };
            for (var i = 1; i < chainCount; i++)
            {
                var r = GetCloseTransform(result[i - 1]);
                if (r == null) return result.ToArray();
                result.Add(r);
            }

            return result.ToArray();
        }

        private Transform GetCloseTransform(Transform target)
        {
            var enemies = new List<Transform>();
            foreach (var e in _enemiesFilter)
                enemies.Add(_enemiesFilter.Get2(e).View.CharacterController.transform);
            var result = enemies[0];
            foreach (var enemy in enemies.Where(enemy =>
                         Vector3.Distance(enemy.position, target.position) <
                         Vector3.Distance(result.position, target.position) && target != enemy))
                result = enemy;

            return result;
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