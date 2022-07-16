using Gameplay.Components.Share.Attack;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Share
{
    public class ReloadAttackTimerSystem : IEcsRunSystem
    {
        private EcsFilter<ReloadAttackTimer> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var reloadAttackTimer = ref _filter.Get1(i);
                reloadAttackTimer.Time -= Time.deltaTime;

                if (reloadAttackTimer.Time <= 0)
                {
                    var entity = _filter.GetEntity(i);
                    entity.Del<ReloadAttackTimer>();
                    entity.Get<CanAttackTag>();
                }
            }
        }
    }
}