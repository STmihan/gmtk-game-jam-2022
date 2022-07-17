using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Gameplay.Components.Camera;
using Gameplay.Components.Enemy;
using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Components.Share.Attack;
using Gameplay.Configs.Attacks;
using Gameplay.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player.Attack.Types
{
    public class PlayerDarkAttackSystem : IEcsRunSystem
    {
        private EcsFilter<HitEvent> _filter;
        private EcsFilter<EnemyTag, CharacterViewComponent, HpComponent> _enemiesFilter;
        private EcsFilter<PlayerTag, CharacterViewComponent> _playerFilter;
        private AttackConfig _config;
        private EcsWorld _world;

        public void Run()
        {
            foreach (var p in _playerFilter)
            foreach (var i in _filter)
            {
                var hit = _filter.Get1(i);
                var type = hit.Type;
                if (type != typeof(PlayerDarkAttack)) continue;
                var config = (PlayerDarkAttack)_config.Attacks.FirstOrDefault(attack => attack.Type == type);
                if (config == null) continue;
                var playerTransform = _playerFilter.Get2(p).View.transform;
                var playerPos = playerTransform.position;
                var chargeVFX = Object.Instantiate(
                    config.ChargeVFX,
                    new Vector3(playerPos.x, 1f, playerPos.z),
                    Quaternion.identity);
                chargeVFX.transform.SetParent(playerTransform);
                var sequence = DOTween.Sequence();
                sequence.Join(chargeVFX.transform.DOScaleX(config.Radius, config.TimeBeforeHit));
                sequence.Join(chargeVFX.transform.DOScaleZ(config.Radius, config.TimeBeforeHit));
                sequence.Join(chargeVFX.transform.DOScaleY(1, config.TimeBeforeHit));
                sequence.OnComplete(() =>
                {
                    Collider[] result = new Collider[20];
                    var aloc = Physics.OverlapSphereNonAlloc(playerTransform.position, config.Radius, result, LayerMask.GetMask("Enemies"));
                    foreach (var collider in result)
                    {
                        if (collider == null) continue;
                        if (collider.TryGetComponent(out CharacterView view))
                        {
                            var enemyEntity = GetEnemyByTransform(view.transform);
                            ref var hpComponent = ref enemyEntity.Get<HpComponent>();
                            hpComponent.Hp -= config.Damage;
                            if(aloc > 3) 
                                _world.NewEntity().Get<CameraShakeComponent>();
                            Object.Instantiate(config.ExplosionVFX, view.transform.position, Quaternion.identity);
                        }
                    }
                    Object.Destroy(chargeVFX.gameObject);
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