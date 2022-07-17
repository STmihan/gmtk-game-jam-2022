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
using UnityEngine;

namespace Gameplay.Systems.Enemy.Attack
{
    public class FireChargeEnemyAttackSystem : IEcsRunSystem
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
                if (type != typeof(MeleeEnemy)) continue;
                var entity = _filter.GetEntity(i);
                var view = _filter.Get2(i).View;
                var config =
                    (EnemyFireChargeAttack)_attackConfig.Attacks.FirstOrDefault(attack =>
                        attack.Type == typeof(EnemyFireChargeAttack));
                var statsConfig = _statsConfig.GetConfigByType<MeleeEnemy>(type);

                // Start Animation
                var playerPos = _playerFilter.Get2(p).View.transform.position;
                var endPoint = view.transform.position +
                               (playerPos - view.transform.position).normalized * statsConfig.ChargeDistance;


                var startPos = view.transform.position;
                entity.Get<IsAttackingTimer>().Time = statsConfig.DelayBeforeCharge;
                var sequence = DOTween.Sequence();
                var trail = Object.Instantiate(config.TrailVFX, view.transform.position, Quaternion.identity);
                trail.transform.SetParent(view.transform);
                Object.Destroy(trail.gameObject, statsConfig.DelayBeforeCharge + 1f);
                sequence.AppendInterval(statsConfig.DelayBeforeCharge);
                sequence
                    .Append(view.transform.DOMove(endPoint, 0.2f))
                    .OnComplete(() =>
                    {
                        endPoint.y = 1;
                        startPos.y = 1;
                        var ray = new Ray(startPos,
                            (endPoint - startPos).normalized * statsConfig.ChargeDistance);
                        if (Physics.Raycast(ray, out _, statsConfig.ChargeDistance, LayerMask.GetMask("Player")))
                        {
                            ref var hpComponent = ref _playerFilter.Get3(p);
                            hpComponent.Hp -= statsConfig.Damage;
                            _world.NewEntity().Get<CameraShakeComponent>();
                        }
                    });

                entity.Del<CanAttackTag>();
                ref var reloadAttackTimer = ref entity.Get<ReloadAttackTimer>();
                reloadAttackTimer.Time = statsConfig.DelayBetweenHits;
            }
        }
    }
}