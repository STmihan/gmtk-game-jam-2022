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
        private EcsFilter<PlayerActiveWeaponComponent> _activeWeapon;
        private EcsFilter<PlayerTag, CharacterViewComponent, CanAttackTag> _playerFilter;
        private EcsWorld _world;
        private AttackConfig _config;
        private PlayerConfig _playerConfig;

        public void Run()
        {
            foreach (var e in _attackEventFilter)
            foreach (var i in _playerFilter)
            foreach (var j in _activeWeapon)
            {
                var weapon = _activeWeapon.Get1(j).Id;
                var entity = _world.NewEntity();
                var config = _config.Attacks[weapon];
                var firePoint = _playerFilter.Get2(i).View.FirePoint;
                var direction = firePoint.forward;
                var view = Object.Instantiate(config.PlayerProjectilePrefab, firePoint.position, Quaternion.identity);
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
                attackEvent.PlayerProjectileView = view;

                var playerEntity = _playerFilter.GetEntity(i);
                playerEntity.Del<CanAttackTag>();
                ref var reloadAttackTimer = ref playerEntity.Get<ReloadAttackTimer>();
                reloadAttackTimer.Time = _playerConfig.PlayerAttackDelay;
            }
        }
    }
}