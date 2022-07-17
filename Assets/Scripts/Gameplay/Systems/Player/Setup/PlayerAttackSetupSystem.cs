using System;
using System.Collections.Generic;
using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Components.Share.Attack;
using Gameplay.Configs.Attacks;
using Leopotam.Ecs;

namespace Gameplay.Systems.Player.Setup
{
    public class PlayerAttackSetupSystem : IEcsInitSystem
    {
        private EcsFilter<PlayerTag, CharacterViewComponent> _filter; 
        public void Init()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                entity.Get<CanAttackTag>();
                ref var playerActiveWeaponComponent = ref entity.Get<PlayerActiveWeaponComponent>();
                playerActiveWeaponComponent.Id = 0;
                ref var playerDicesCountComponent = ref entity.Get<PlayerDicesCountComponent>();
                playerDicesCountComponent.Value = new Dictionary<Type, int>();
                playerDicesCountComponent.Value.Add(typeof(PlayerBlueAttack), 10);
                playerDicesCountComponent.Value.Add(typeof(PlayerBrownAttack), 10);
                playerDicesCountComponent.Value.Add(typeof(PlayerFireAttack), 10);
                playerDicesCountComponent.Value.Add(typeof(PlayerDarkAttack), 10);
            }
        }
    }
}