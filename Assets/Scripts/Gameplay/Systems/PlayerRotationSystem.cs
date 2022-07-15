using Gameplay.Components;
using Leopotam.Ecs;

namespace Gameplay.Systems
{
    public class PlayerRotationSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerTag, GameObjectComponent, RotationComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var gameObjectComponent = ref _filter.Get2(i);
                ref var rotationComponent = ref _filter.Get3(i);
                var transform = gameObjectComponent.GameObject.transform;
                transform.LookAt(rotationComponent.InDirection);
            }
        }
    }
}