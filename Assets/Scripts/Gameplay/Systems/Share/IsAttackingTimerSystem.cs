using Gameplay.Components.Enemy;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Share
{
    public class IsAttackingTimerSystem : IEcsRunSystem
    {
        private EcsFilter<IsAttackingTimer> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var isAttackingTimer = ref _filter.Get1(i);
                isAttackingTimer.Time -= Time.deltaTime;
                if(isAttackingTimer.Time <= 0) _filter.GetEntity(i).Del<IsAttackingTimer>();
            }
        }
    }
}