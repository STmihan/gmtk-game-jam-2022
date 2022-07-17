using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Gameplay.Components.Enemy;
using Gameplay.Components.Share;
using Gameplay.Components.Share.Attack;
using Gameplay.Configs.Attacks;
using Gameplay.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player.Attack.Types
{
    public class PlayerFireAttackSystem : IEcsRunSystem
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
                if(type != typeof(PlayerFireAttack)) continue;
                var config = (PlayerFireAttack)_config.Attacks.FirstOrDefault(attack => attack.Type == type);
                if (config == null) continue;
                var vfxPrefab = config.ExplosionVFX;
                var explosion = Object.Instantiate(vfxPrefab, hit.Position, Quaternion.identity);
                var circleVfx = Object.Instantiate(config.CircleVfx, hit.Position, Quaternion.identity);
                var sequence = DOTween.Sequence();
                sequence.Join(circleVfx.transform.DOScaleX(config.Radius, .4f));
                sequence.Join(circleVfx.transform.DOScaleY(1, .4f));
                sequence.Join(circleVfx.transform.DOScaleZ(config.Radius, .4f));
                         
                Collider[] result = new Collider[20];
                
                Physics.OverlapSphereNonAlloc(hit.Position, config.Radius, result, LayerMask.GetMask("Enemies"));
                foreach (var collider in result)
                {
                    if (collider == null) continue;
                    if (collider.TryGetComponent(out CharacterView view))
                    {
                        var enemyEntity = GetEnemyByTransform(view.transform);
                        ref var hpComponent = ref enemyEntity.Get<HpComponent>();
                        hpComponent.Hp -= config.Damage;
                        Object.Instantiate(config.ExplosionVFX, view.transform.position, Quaternion.identity);
                    }
                }
                sequence.OnComplete(() =>
                {
                    Object.Destroy(circleVfx.gameObject);
                    Object.Destroy(explosion.gameObject);
                });
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
    }
}