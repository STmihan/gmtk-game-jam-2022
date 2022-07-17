using Gameplay.Components.Enemy;
using Gameplay.Components.Player;
using Gameplay.Configs;
using Leopotam.Ecs;
using UnityEngine.SceneManagement;

namespace Gameplay.Systems.Player
{
    public class PlayerHpSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<PlayerTag> _filter;
        private PlayerConfig _config;

        public void Init()
        {
            foreach (var i in _filter)
            {
                ref var hp = ref _filter.GetEntity(i).Get<HpComponent>();
                hp.MaxHp = _config.PlayerMaxHp;
                hp.Hp = _config.PlayerMaxHp;
            }
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                var hp = _filter.GetEntity(i)
                                .Get<HpComponent>()
                                .Hp;
                if (hp <= 0)
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }
}