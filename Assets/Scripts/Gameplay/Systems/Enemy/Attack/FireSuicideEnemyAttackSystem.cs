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
    public class FireSuicideEnemyAttackSystem : IEcsRunSystem
    {
        private const float CIRCLE_DURATION = .3f;
        private EcsFilter<EnemyTag, CharacterViewComponent, CanAttackTag> _filter;
        private EcsFilter<PlayerTag, HpComponent> _playerFilter;
        private AttackConfig _attackConfig;
        private EnemyStatsConfig _statsConfig;
        private EcsWorld _world;

        public void Run()
        {
            foreach (var p in _playerFilter)
            foreach (var i in _filter)
            {
                var type = _filter.Get1(i).Type;
                if (type != typeof(SuicideMeleeEnemy)) continue;
                var view = _filter.Get2(i).View;
                var hitPoint = view.transform.position;
                var config =
                    (EnemyFireSuicideAttack)_attackConfig.Attacks.FirstOrDefault(attack =>
                        attack.Type == typeof(EnemyFireSuicideAttack));
                var statsConfig = _statsConfig.GetConfigByType<SuicideMeleeEnemy>(type);
                
                var hitVFX = Object.Instantiate(config.ExplosionVFX, hitPoint, Quaternion.identity);
                var circleVFX = Object.Instantiate(config.CircleVFX, hitPoint, Quaternion.identity);
                var sequence = DOTween.Sequence();
                sequence.Join(circleVFX.transform.DOScaleX(statsConfig.AoeRadius, CIRCLE_DURATION));
                sequence.Join(circleVFX.transform.DOScaleY(1, CIRCLE_DURATION));
                sequence.Join(circleVFX.transform.DOScaleZ(statsConfig.AoeRadius, CIRCLE_DURATION));
                sequence.OnComplete(() => Object.Destroy(circleVFX.gameObject));
                Object.Destroy(hitVFX.gameObject, 1);
                Collider[] result = new Collider[2];
                Physics.OverlapSphereNonAlloc(hitPoint, statsConfig.AoeRadius, result, LayerMask.GetMask("Player"));
                foreach (var collider in result)
                {
                    if (collider == null) continue;
                    if (collider.TryGetComponent(out CharacterView g))
                    {
                        ref var hpComponent = ref _playerFilter.Get2(p);
                        hpComponent.Hp -= statsConfig.Damage;
                        _world.NewEntity().Get<CameraShakeComponent>();
                    }
                }
                Object.Destroy(view.gameObject);
                _filter.GetEntity(i).Destroy();
            }
        }
    }
}