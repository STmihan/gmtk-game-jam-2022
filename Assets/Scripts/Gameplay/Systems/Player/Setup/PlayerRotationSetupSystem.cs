using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Configs;
using Leopotam.Ecs;

namespace Gameplay.Systems.Player.Movement
{
    public class PlayerRotationSetupSystem : IEcsInitSystem
    {
        private PlayerConfig _config;
        private EcsFilter<PlayerTag, CharacterViewComponent> _filter;
        public void Init()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                entity.Get<RotationComponent>();
            }
        }
    }
}