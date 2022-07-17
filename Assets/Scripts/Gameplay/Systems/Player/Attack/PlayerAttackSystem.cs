using DG.Tweening;
using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Components.Share.Attack;
using Gameplay.Configs;
using Gameplay.Configs.Attacks;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player.Attack
{
    public class PlayerAttackSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerInputAttackEvent> _attackEventFilter;
        private EcsFilter<PlayerInputSecondaryAttackEvent> _secondaryAttackEventFilter;
        private EcsFilter<PlayerActiveWeaponComponent> _activeWeapon;
        private EcsFilter<PlayerTag, CharacterViewComponent, CanAttackTag> _playerFilter;
        private EcsFilter<PlayerDicesCountComponent> _diceCountFilter;
        private EcsWorld _world;
        private AttackConfig _config;
        private PlayerConfig _playerConfig;

        public void Run()
        {
            foreach (var i in _activeWeapon)
            {
                foreach (var e in _attackEventFilter) Shoot(_activeWeapon.Get1(i).Id);
            }

            foreach (var s in _secondaryAttackEventFilter) Shoot(4);
        }

        private void Shoot(int weapon)
        {
            foreach (var j in _diceCountFilter)
            foreach (var i in _playerFilter)
            {
                var config = _config.Attacks[weapon];
                var type = config.Type;
                var dic = _diceCountFilter.Get1(j).Value;
                if (dic.ContainsKey(type))
                {
                    if (dic[type] <= 0) continue;
                    dic[type] -= 1;
                }
                var characterView = _playerFilter.Get2(i).View;
                characterView.Animator.SetTrigger("Attack");
                characterView.Animator.SetFloat("AttackSpeed", 1/0.2f);
                DOTween.Sequence().AppendInterval(0.2f).OnComplete(() =>
                {
                    var entity = _world.NewEntity();
                    var firePoint = characterView.FirePoint;
                    var direction = firePoint.forward;
                    var view = Object.Instantiate(config.ProjectilePrefab, firePoint.position, Quaternion.identity);
                    view.Type = config.Type;
                    view.LayerMask = LayerMask.GetMask("Enemies");
                    view.EcsWorld = _world;
                    view.Speed = _playerConfig.PlayerProjectileSpeed;
                    view.Direction = direction;
                    if (config.Type == typeof(PlayerBrownAttack)) view.DestroyOnHit = false;
                    ref var attackEvent = ref entity.Get<AttackEvent>();
                    attackEvent.Direction = direction;
                    attackEvent.Type = config.Type;
                    attackEvent.LayerMask = view.LayerMask;
                    attackEvent.ProjectileView = view;
                });
                var playerEntity = _playerFilter.GetEntity(i);
                playerEntity.Del<CanAttackTag>();
                ref var reloadAttackTimer = ref playerEntity.Get<ReloadAttackTimer>();
                reloadAttackTimer.Time = _playerConfig.PlayerAttackDelay;
            }
        }
    }
}