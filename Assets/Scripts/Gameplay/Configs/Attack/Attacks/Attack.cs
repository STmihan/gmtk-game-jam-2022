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
        public PlayerProjectileView PlayerProjectilePrefab;
        [OdinSerialize] public Type Type => GetType();
    }
}