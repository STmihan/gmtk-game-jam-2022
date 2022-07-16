using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Leopotam.Ecs;

namespace Gameplay.Systems.Player.Movement
{
    public class PlayerMovementSetupSystem : IEcsInitSystem
    {
        private EcsFilter<PlayerTag, CharacterViewComponent> _filter;
        public void Init()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                entity.Get<MovementComponent>();
            }
        }
    }
}