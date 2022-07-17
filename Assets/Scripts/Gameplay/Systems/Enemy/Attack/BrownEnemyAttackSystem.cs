using System.Linq;
using DG.Tweening;
using Gameplay.Components.Camera;
using Gameplay.Components.Enemy;
using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Components.Share.Attack;
using Gameplay.Configs.Attacks;
using Gameplay.Configs.Enemies;
using Gameplay.Configs.Enemies.Stats;
using Gameplay.UnityComponents;
using Leopotam.Ecs;
using Music;
using UnityEngine;

namespace Gameplay.Systems.Enemy.Attack
{
    public class BrownEnemyAttackSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<EnemyTag, CharacterViewComponent, CanAttackTag> _filter;
        private EcsFilter<PlayerTag, CharacterViewComponent, HpComponent> _playerFilter;
        private AttackConfig _attackConfig;
        private EnemyStatsConfig _statsConfig;

        public void Run()
        {
            foreach (var p in _playerFilter)
            foreach (var i in _filter)
            {
                var type = _filter.Get1(i).Type;
                if (type != typeof(RangedAoeEnemy)) continue;
                var entity = _filter.GetEntity(i);
                var view = _filter.Get2(i).View;
                var hitPoint = view.FirePoint.position;
                var config =
                    (EnemyBrownAttack)_attackConfig.Attacks.FirstOrDefault(attack =>
                        attack.Type == typeof(EnemyBrownAttack));
                var statsConfig = _statsConfig.GetConfigByType<RangedAoeEnemy>(type);
                var playerPos = _playerFilter.Get2(p).View.transform.position;
                // Start Animation
                view.Animator.SetTrigger("Attack");
                view.Animator.SetFloat("AttackSpeed", 1 / 0.4f);
                var sequence = DOTween.Sequence();
                sequence.AppendCallback(() => { entity.Get<IsAttackingTimer>().Time = 0.4f; });
                sequence.AppendInterval(0.4f).OnComplete(() =>
                {
                    SoundController.Play(config.ThrowSound);
                    var shootVfx = Object.Instantiate(config.ShootVFX, hitPoint, view.FirePoint.rotation);
                    shootVfx.transform.Rotate(shootVfx.transform.right, -90f);
                    shootVfx.transform.SetParent(view.FirePoint);
                    Object.Destroy(shootVfx, 1f);
                    var bottomVfx = Object.Instantiate(config.BottomVfx, playerPos, Quaternion.identity);
                    var topVfx = Object.Instantiate(config.TopVFX, new Vector3(playerPos.x + 10, 15, playerPos.z + 10),
                        Quaternion.identity);
                    var sequence = DOTween.Sequence();
                    sequence.Append(Object
                                    .Instantiate(config.TopVFX, hitPoint, Quaternion.identity).transform
                                    .DOMove(hitPoint + view.FirePoint.up * 10, statsConfig.DelayBeforeHit * .3f));
                    sequence.Append(bottomVfx.transform.DOScaleX(statsConfig.AoeRadius,
                        statsConfig.DelayBeforeHit * .7f));
                    sequence.Join(bottomVfx.transform.DOScaleY(1, statsConfig.DelayBeforeHit * .7f));
                    sequence.Join(bottomVfx.transform.DOScaleZ(statsConfig.AoeRadius,
                        statsConfig.DelayBeforeHit * .7f));
                    sequence.Join(topVfx.transform.DOMove(playerPos, statsConfig.DelayBeforeHit * 0.7f));
                    sequence.Join(topVfx.transform.DOScale(Vector3.one * (statsConfig.AoeRadius * 0.9f),
                        statsConfig.DelayBeforeHit * 0.7f));
                    sequence.OnComplete(() =>
                    {
                        Object.Destroy(bottomVfx.gameObject);
                        Object.Destroy(topVfx.gameObject);
                        Object.Destroy(Object.Instantiate(config.HitVFX, playerPos, Quaternion.identity), .7f);
                        Hit(hitPoint, statsConfig, ref _playerFilter.Get3(p), config);
                    });
                });


                entity.Del<CanAttackTag>();
                ref var reloadAttackTimer = ref entity.Get<ReloadAttackTimer>();
                reloadAttackTimer.Time = statsConfig.DelayBetweenHits;
            }
        }

        private void Hit(Vector3 hitPoint, RangedAoeEnemy statsConfig, ref HpComponent hpComponent, EnemyBrownAttack config)
        {
            var result = new Collider[2];
            Physics.OverlapSphereNonAlloc(hitPoint, statsConfig.AoeRadius, result,
                LayerMask.GetMask("Player"));
            foreach (var collider in result)
            {
                SoundController.Play(config.HitSound);
                if (collider == null) continue;
                hpComponent.Hp -= statsConfig.Damage;
                _world.NewEntity().Get<CameraShakeComponent>();
            }
        }
    }
}