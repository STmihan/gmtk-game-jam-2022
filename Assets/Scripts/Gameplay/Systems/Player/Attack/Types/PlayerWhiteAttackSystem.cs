using System.Linq;
using Gameplay.Components.Share.Attack;
using Gameplay.Configs.Attacks;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player.Attack.Types
{
    public class PlayerWhiteAttackSystem : IEcsRunSystem
    {
        private EcsFilter<HitEvent> _filter;
        private AttackConfig _config;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                var hit = _filter.Get1(i);
                var type = hit.Type;
                if(type != typeof(PlayerWhiteAttack)) return;
                var config = (PlayerWhiteAttack)_config.Attacks.FirstOrDefault(attack => attack.Type == type);
                if (config == null) continue;
                var vfxPrefab = config.HitVFX;
                Object.Instantiate(vfxPrefab, hit.Position, Quaternion.identity);
                _filter.GetEntity(i).Del<HitEvent>();
            }
        }
    }
}