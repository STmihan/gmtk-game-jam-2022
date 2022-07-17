using Gameplay.Components.Enemy;
using Gameplay.Components.Share;
using Gameplay.Configs.Attacks;
using Gameplay.Configs.Enemies.Stats;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Enemy
{
    public class EnemyDieSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<EnemyTag, CharacterViewComponent, HpComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var hp = _filter.Get3(i).Hp;
                if (hp <= 0)
                {
                    Object.Destroy(_filter.Get2(i).View.gameObject);
                    ref var onEnemyDieEvent = ref _world.NewEntity()
                                                        .Get<OnEnemyDieEvent>();
                    ref var enemyTag = ref _filter.Get1(i);
                    if (enemyTag.Type == typeof(MeleeEnemy))
                    {
                        onEnemyDieEvent.Type = typeof(PlayerFireAttack);
                        onEnemyDieEvent.Count = 2;
                    }

                    if (enemyTag.Type == typeof(SuicideMeleeEnemy))
                    {
                        onEnemyDieEvent.Type = typeof(PlayerFireAttack);
                        onEnemyDieEvent.Count = 2;
                    }

                    if (enemyTag.Type == typeof(BigMeleeEnemy))
                    {
                        onEnemyDieEvent.Type = typeof(PlayerDarkAttack);
                        onEnemyDieEvent.Count = 2;
                    }

                    if (enemyTag.Type == typeof(RangedAoeEnemy))
                    {
                        onEnemyDieEvent.Type = typeof(PlayerBrownAttack);
                        onEnemyDieEvent.Count = 2;
                    }

                    if (enemyTag.Type == typeof(RangedProjectileEnemy))
                    {
                        onEnemyDieEvent.Type = typeof(PlayerBlueAttack);
                        onEnemyDieEvent.Count = 2;
                    }
                    _filter.GetEntity(i).Destroy();
                }
            }
        }
    }
}