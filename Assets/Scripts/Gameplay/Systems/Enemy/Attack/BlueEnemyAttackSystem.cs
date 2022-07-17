using System.Linq;
using DG.Tweening;
using Gameplay.Components.Enemy;
using Gameplay.Components.Share;
using Gameplay.Components.Share.Attack;
using Gameplay.Configs.Attacks;
using Gameplay.Configs.Enemies;
using Gameplay.Configs.Enemies.Stats;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Enemy.Attack
{
    public class BlueEnemyAttackSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<EnemyTag, CharacterViewComponent, CanAttackTag> _filter;
        private AttackConfig _attackConfig;
        private EnemyStatsConfig _statsConfig;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var type = _filter.Get1(i).Type;
                if (type != typeof(RangedProjectileEnemy)) continue;
                var entity = _filter.GetEntity(i);
                var view = _filter.Get2(i).View;
                var direction = view.FirePoint.forward;
                var config =
                    (EnemyBlueAttack)_attackConfig.Attacks.FirstOrDefault(attack =>
                        attack.Type == typeof(EnemyBlueAttack));
                var statsConfig = _statsConfig.GetConfigByType<RangedProjectileEnemy>(type);
                view.Animator.SetTrigger("Attack");
                view.Animator.SetFloat("AttackSpeed", 1 / 0.4f);
                var sequence = DOTween.Sequence();
                sequence.AppendCallback(() =>
                {
                    entity.Get<IsAttackingTimer>().Time = 0.4f;
                });
                sequence.AppendInterval(0.4f).OnComplete(() =>
                {
                    var projectileView = Object.Instantiate(config!.ProjectilePrefab, view.FirePoint.position,
                        Quaternion.identity);
                    projectileView.Type = config.Type;
                    projectileView.LayerMask = LayerMask.GetMask("Player");
                    projectileView.EcsWorld = _world;
                    projectileView.Speed = statsConfig.ProjectileSpeed;
                    projectileView.Direction = direction;

                    ref var attackEvent = ref entity.Get<AttackEvent>();
                    attackEvent.Direction = direction;
                    attackEvent.Type = config.Type;
                    attackEvent.LayerMask = LayerMask.GetMask("Player");
                    attackEvent.ProjectileView = projectileView;
                });

                entity.Del<CanAttackTag>();
                ref var reloadAttackTimer = ref entity.Get<ReloadAttackTimer>();
                reloadAttackTimer.Time = statsConfig.DelayBetweenHits;
            }
        }
    }
}