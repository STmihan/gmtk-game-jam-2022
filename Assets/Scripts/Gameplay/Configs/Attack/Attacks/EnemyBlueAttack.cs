using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;
using Object = UnityEngine.Object;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public class EnemyBlueAttack : Attack
    {
        public VFXView HitVFX;
    }
}