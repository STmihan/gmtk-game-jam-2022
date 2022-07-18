using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public abstract class Attack
    {
        [PreviewField]
        public ProjectileView ProjectilePrefab;
        [OdinSerialize] public Type Type => GetType();
    }
}