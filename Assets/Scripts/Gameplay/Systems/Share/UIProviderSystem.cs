using Gameplay.Components.Enemy;
using Gameplay.Components.Player;
using Gameplay.Configs.Attacks;
using Leopotam.Ecs;
using UI;

namespace Gameplay.Systems.Share
{
    public class UIProviderSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerTag, HpComponent, PlayerActiveWeaponComponent, PlayerDicesCountComponent> _filter;
        private GameplayUIProvider _uiProvider;
        public void Run()
        {
            foreach (var i in _filter)
            {
                var hpComponent = _filter.Get2(i);
                var playerDicesCountComponent = _filter.Get4(i);
                var activeWeaponComponent = _filter.Get3(i);
                _uiProvider.MaxHp = hpComponent.MaxHp;
                _uiProvider.Hp = hpComponent.Hp;
                _uiProvider.FireDiceCount = playerDicesCountComponent.Value[typeof(PlayerFireAttack)];
                _uiProvider.LightDiceCount = playerDicesCountComponent.Value[typeof(PlayerBlueAttack)];
                _uiProvider.DarkDiceCount = playerDicesCountComponent.Value[typeof(PlayerDarkAttack)];
                _uiProvider.EarthDiceCount = playerDicesCountComponent.Value[typeof(PlayerBrownAttack)];
                _uiProvider.CurrentDice = activeWeaponComponent.Id;
            }
        }
    }
}