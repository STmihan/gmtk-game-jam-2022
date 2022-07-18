using DG.Tweening;
using Gameplay.Components.Enemy;
using Gameplay.Components.Share;
using Leopotam.Ecs;

namespace Gameplay.Systems.Enemy.Movement
{
    public class EnemyRotationSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyTag, RotationComponent, CharacterViewComponent> _enemyFilter;

        public void Run()
        {
            foreach (var i in _enemyFilter)
            {
                var entity = _enemyFilter.GetEntity(i);
                if (entity.Has<IsAttackingTimer>()) return;
                var rotationComponent = _enemyFilter.Get2(i);
                var enemyTransform = _enemyFilter.Get3(i).View.transform;
                enemyTransform.DOLookAt(rotationComponent.InDirection, rotationComponent.Duration);
            }
        }
    }
}