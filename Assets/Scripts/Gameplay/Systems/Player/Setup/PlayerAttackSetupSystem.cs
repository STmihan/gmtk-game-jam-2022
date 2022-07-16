using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Components.Share.Attack;
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
            }
        }
    }
}