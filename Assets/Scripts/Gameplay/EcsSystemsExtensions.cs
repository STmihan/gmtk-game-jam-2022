using Gameplay.Systems.Enemy;
using Gameplay.Systems.Enemy.Movement;
using Gameplay.Systems.Enemy.Setup;
using Gameplay.Systems.Player.Attack;
using Gameplay.Systems.Player.Attack.Types;
using Gameplay.Systems.Player.Input;
using Gameplay.Systems.Player.Movement;
using Gameplay.Systems.Player.Setup;
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
                         .Add(new EnemyRotationSystem())
                         .Add(new EnemyDieSystem());
        }
        
        public static EcsSystems AddPlayerSystems(this EcsSystems system)
        {
            return system.Add(new PlayerInputSystem())
                         .Add(new PlayerViewSetupSystem())
                         .Add(new PlayerMovementSetupSystem())
                         .Add(new PlayerRotationSetupSystem())
                         .Add(new PlayerAttackSetupSystem())
                         .Add(new PlayerSetMoveDirectionSystem())
                         .Add(new PlayerSetRotationDirectionSystem())
                         .Add(new PlayerMovementSystem())
                         .Add(new PlayerRotationSystem())
                         .Add(new PlayerAttackSystem())
                         .Add(new PlayerChangeActiveWeaponSystem())
                         .Add(new PlayerWhiteAttackSystem())
                         .Add(new PlayerBlueAttackSystem())
                         .Add(new PlayerBrownAttackSystem())
                         .Add(new PlayerDarkAttackSystem())
                         .Add(new PlayerFireAttackSystem());
        }
    }
}