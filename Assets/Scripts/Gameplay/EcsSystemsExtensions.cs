using Gameplay.Systems.Enemy;
using Gameplay.Systems.Enemy.Movement;
using Gameplay.Systems.Enemy.Setup;
using Leopotam.Ecs;

namespace Gameplay
{
    public static class EcsSystemsExtensions
    {
        public static EcsSystems AddEnemySystems(this EcsSystems system)
        {
            return system.Add(new EnemyManagerSystem())
                         .Add(new EnemyGenerateSpawnPositionSystem())
                         .Add(new EnemySpawnSystem())
                         .Add(new EnemySetupSystem())
                         .Add(new EnemySetDirectionSystem())
                         .Add(new EnemyMovementSystem())
                         .Add(new EnemyRotationSystem());
        }
    }
}