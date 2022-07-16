using Gameplay.Components.Player;
using Leopotam.Ecs;

namespace Gameplay.Systems.Player.Attack
{
    public class PlayerAttackSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerInputAttackEvent> _attackEventFilter;
        public void Run()
        {
            foreach (var i in _attackEventFilter)
            {
                // _attackEventFilter.
            }
        }
    }
}