using DG.Tweening;
using Gameplay.Components.Camera;
using Gameplay.Components.Enemy;
using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Configs;
using Leopotam.Ecs;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.Systems.Player
{
    public class PlayerHpSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<PlayerTag> _filter;
        private PlayerConfig _config;
        private GameplayUIProvider _uiProvider;
        private EcsWorld _world;

        public void Init()
        {
            foreach (var i in _filter)
            {
                ref var hp = ref _filter.GetEntity(i).Get<HpComponent>();
                hp.MaxHp = _config.PlayerMaxHp;
                hp.Hp = _config.PlayerMaxHp;
            }
        }

        private bool b = false;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                var hp = entity
                         .Get<HpComponent>()
                         .Hp;
                if (hp <= 0 && b == false)
                {
                    b = true;
                    entity.Del<MovementComponent>();
                    _world.NewEntity().Get<CameraShakeComponent>();

                    var sequence = DOTween.Sequence();
                    sequence.Append(DOTween
                                    .To(() => Time.timeScale, x => Time.timeScale = x, 0, 4f)
                                    .SetEase(Ease.Linear).SetUpdate(true));
                    sequence.Join(_uiProvider.DeadScreen.DOFade(1, 4f)).SetUpdate(true);
                    sequence.AppendInterval(1)
                            .SetUpdate(true);
                    sequence.AppendCallback(
                        () =>
                        {
                            Time.timeScale = 1;
                            SceneManager.LoadScene(0);
                        });
                }
            }
        }
    }
}