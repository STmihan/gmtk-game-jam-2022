using Gameplay.Components.Player;
using Gameplay.Components.Share.Attack;
using Leopotam.Ecs;

namespace Gameplay.Systems.Player.Attack
{
    public class PlayerAttackSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerInputAttackEvent> _attackEventFilter;
        private EcsFilter<PlayerTag, CanAttackTag> _playerFilter;
        public void Run()
        {
            foreach (var i in _attackEventFilter)
            {
                foreach (var j in _playerFilter)
                {
                    var entity = _playerFilter.GetEntity(j);
                    
                }
            }
        }
    }
}