using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Leopotam.Ecs;

namespace Gameplay.Systems.Player.Setup
{
    public class PlayerAttackSetupSystem : IEcsInitSystem
    {
        private EcsFilter<PlayerTag, CharacterViewComponent> _filter; 
        public void Init()
        {
            
        }
    }
}