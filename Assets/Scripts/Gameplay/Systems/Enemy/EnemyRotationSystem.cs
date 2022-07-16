using DG.Tweening;
using Gameplay.Components.Enemy;
using Gameplay.Components.Share;
using Leopotam.Ecs;

namespace Gameplay.Systems.Enemy
{
    public class EnemyRotationSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyTag, RotationComponent, GameObjectComponent> _enemyFilter;

        public void Run()
        {
            foreach (var i in _enemyFilter)
            {
                var rotationComponent = _enemyFilter.Get2(i);
                var enemyTransform = _enemyFilter.Get3(i).GameObject.transform;
                enemyTransform.DOLookAt(rotationComponent.InDirection, rotationComponent.Duration);
            }
        }
    }
}