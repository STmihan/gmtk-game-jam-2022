using Gameplay.Components.Enemy;
using Gameplay.Components.Share;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Enemy
{
    public class EnemyDieSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyTag, CharacterViewComponent, HpComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var hp = _filter.Get3(i).Hp;
                if (hp <= 0)
                {
                    Object.Destroy(_filter.Get2(i).View.gameObject);
                    _filter.GetEntity(i).Destroy();
                }
            }
        }
    }
}