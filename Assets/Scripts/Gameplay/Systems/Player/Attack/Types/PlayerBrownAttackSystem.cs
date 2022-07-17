using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Gameplay.Components.Camera;
using Gameplay.Components.Enemy;
using Gameplay.Components.Share;
using Gameplay.Components.Share.Attack;
using Gameplay.Configs.Attacks;
using Gameplay.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player.Attack.Types
{
    public class PlayerBrownAttackSystem : IEcsRunSystem
    {
        private EcsFilter<HitEvent> _filter;
        private EcsFilter<EnemyTag, CharacterViewComponent, HpComponent> _enemiesFilter;
        private AttackConfig _config;
        private EcsWorld _world;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var hit = _filter.Get1(i);
                var type = hit.Type;
                if (type != typeof(PlayerBrownAttack)) continue;
                var config = (PlayerBrownAttack)_config.Attacks.FirstOrDefault(attack => attack.Type == type);
                if (config == null) continue;
                var top = Object.Instantiate(
                    config.TopVFX,
                    new Vector3(hit.Position.x + 5, 15f, hit.Position.z + 5),
                    Quaternion.identity);
                var sequence = DOTween.Sequence();
                sequence.Join(top.transform
                                 .DOMove(hit.Position, config.TimeBeforeHit)
                                 .OnComplete(() => Object.Destroy(top.gameObject, 0.2f)));
                sequence.OnComplete(() =>
                {
                    Object.Instantiate(config.HitVFX, hit.Position, Quaternion.identity);
                    Collider[] result = new Collider[10];
                    Physics.OverlapSphereNonAlloc(hit.Position, config.Radius, result, LayerMask.GetMask("Enemies"));
                    foreach (var collider in result)
                    {
                        if (collider == null) continue;
                        if (collider.TryGetComponent(out CharacterView view))
                        {
                            var enemyEntity = GetEnemyByTransform(view.transform);
                            ref var hpComponent = ref enemyEntity.Get<HpComponent>();
                            hpComponent.Hp -= config.Damage;
                            _world.NewEntity().Get<CameraShakeComponent>();
                        }
                    }
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