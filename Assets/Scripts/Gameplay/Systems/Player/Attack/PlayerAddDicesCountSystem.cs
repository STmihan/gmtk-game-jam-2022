using Gameplay.Components.Enemy;
using Gameplay.Components.Player;
using Leopotam.Ecs;

namespace Gameplay.Systems.Player.Attack
{
    public class PlayerAddDicesCountSystem : IEcsRunSystem
    {
        private EcsFilter<OnEnemyDieEvent> _dieEvent;
        private EcsFilter<PlayerDicesCountComponent> _dicesFilter;
        public void Run()
        {
            foreach (var i in _dieEvent)
            foreach (var j in _dicesFilter)
            {
                var onEnemyDieEvent = _dieEvent.Get1(i);
                ref var playerDicesCountComponent = ref _dicesFilter.Get1(j);
                if(playerDicesCountComponent.Value.ContainsKey(onEnemyDieEvent.Type))
                    playerDicesCountComponent.Value[onEnemyDieEvent.Type] += onEnemyDieEvent.Count;
                _dieEvent.GetEntity(i).Destroy();
            }
        }
    }
}