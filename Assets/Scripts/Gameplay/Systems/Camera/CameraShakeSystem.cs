using Cinemachine;
using DG.Tweening;
using Gameplay.Components.Camera;
using Leopotam.Ecs;

namespace Gameplay.Systems.Camera
{
    public class CameraShakeSystem : IEcsRunSystem
    {
        private EcsFilter<CameraShakeComponent> _filter;
        private CinemachineVirtualCamera _camera;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var sequence = DOTween.Sequence();
                sequence.AppendCallback(() =>
                    _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 2);
                sequence.AppendInterval(0.2f);
                sequence.AppendCallback(() =>
                    _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0);

                _filter.GetEntity(i).Destroy();
            }
        }
    }
}