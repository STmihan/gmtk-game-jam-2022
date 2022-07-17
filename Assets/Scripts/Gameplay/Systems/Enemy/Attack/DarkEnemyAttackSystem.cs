using System;
using System.Linq;
using DG.Tweening;
using Gameplay.Components.Enemy;
using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Components.Share.Attack;
using Gameplay.Configs.Attacks;
using Gameplay.Configs.Enemies;
using Gameplay.Configs.Enemies.Stats;
using Gameplay.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.Systems.Enemy.Attack
{
    public class DarkEnemyAttackSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<EnemyTag, CharacterViewComponent, CanAttackTag> _filter;
        private EcsFilter<PlayerTag, HpComponent> _playerFilter;
        private AttackConfig _attackConfig;
        private EnemyStatsConfig _statsConfig;
        
        public void Run()
        {
            foreach (var p in _playerFilter)
            foreach (var i in _filter)
            {
                var type = _filter.Get1(i).Type;
                if (type != typeof(BigMeleeEnemy)) continue;
                var entity = _filter.GetEntity(i);
                var view = _filter.Get2(i).View;
                var hitPoint = view.FirePoint.position;
                var config = (EnemyDarkAttack)_attackConfig.Attacks.FirstOrDefault(attack => attack.Type == typeof(EnemyDarkAttack));
                var statsConfig = _statsConfig.GetConfigByType<BigMeleeEnemy>(type);
                // Start Animation
                entity.Get<IsAttackingTimer>().Time = statsConfig.DelayBeforeHit;
                var sequence = DOTween.Sequence();
                sequence.AppendInterval(statsConfig.DelayBeforeHit);
                sequence.OnComplete(() =>
                {
                    var hitVFX = Object.Instantiate(config.HitVFX, hitPoint, Quaternion.identity);
                    Object.Destroy(hitVFX.gameObject, 1);
                    Collider[] result = new Collider[2];
                    Physics.OverlapSphereNonAlloc(hitPoint, statsConfig.AoeRadius, result, LayerMask.GetMask("Player"));
                    foreach (var collider in result)
                    {
                        if (collider == null) continue;
                        if (collider.TryGetComponent(out CharacterView view))
                        {
                            ref var hpComponent = ref _playerFilter.Get2(p);
                            hpComponent.Hp -= statsConfig.Damage;
                        }
                    }
                    
                    var sequence2 = DOTween.Sequence();
                    var chargeVFX = Object.Instantiate(
                        config.ChargeVfx, 
                        new Vector3(
                            view.FirePoint.transform.position.x,
                            0.1f,
                            view.FirePoint.transform.position.z), 
                        Quaternion.identity);
                    sequence2.Join(chargeVFX.transform.DOScaleX(statsConfig.AoeRadius, statsConfig.DelayBeforeExplosion));
                    sequence2.Join(chargeVFX.transform.DOScaleY(1, statsConfig.DelayBeforeExplosion));
                    sequence2.Join(chargeVFX.transform.DOScaleZ(statsConfig.AoeRadius, statsConfig.DelayBeforeExplosion));
                    sequence2.OnComplete(() =>
                    {
                        Object.Destroy(chargeVFX.gameObject);
                        var hitVFX = Object.Instantiate(config.HitVFX, hitPoint, Quaternion.identity);
                        Object.Destroy(hitVFX.gameObject, 1);
                        Collider[] result = new Collider[2];
                        Physics.OverlapSphereNonAlloc(hitPoint, statsConfig.AoeRadius, result, LayerMask.GetMask("Player"));
                        foreach (var collider in result)
                        {
                            if (collider == null) continue;
                            if (collider.TryGetComponent(out CharacterView view))
                            {
                                ref var hpComponent = ref _playerFilter.Get2(p);
                                hpComponent.Hp -= statsConfig.Damage;
                            }
                        }
                    });
                });

                entity.Del<CanAttackTag>();
                ref var reloadAttackTimer = ref entity.Get<ReloadAttackTimer>();
                reloadAttackTimer.Time = statsConfig.DelayBetweenHits;
            }
        }
    }
}