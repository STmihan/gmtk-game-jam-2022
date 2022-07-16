using Gameplay.Components.Player;
using Leopotam.Ecs;

namespace Gameplay.Systems.Player.Attack
{
    public class PlayerChangeActiveWeaponSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerInputChangeWeaponEvent> _event;
        private EcsFilter<PlayerTag, PlayerActiveWeaponComponent> _filter;
        public void Run()
        {
            foreach (var e in _event)
            foreach (var i in _filter)
            {
                ref var playerActiveWeaponComponent = ref _filter.Get2(i);
                playerActiveWeaponComponent.Id = _event.Get1(e).Id;
            }
        }
    }
}