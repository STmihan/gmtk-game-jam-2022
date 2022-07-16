using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public class EnemyBlueAttack : Attack
    {
        [PreviewField]
        public VFXView TrailView;
    }
}