using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public abstract class Attack
    {
        [PreviewField]
        public ProjectileView ProjectilePrefab;
        public AudioClip ThrowSound;
        public AudioClip HitSound;
        [OdinSerialize] public Type Type => GetType();
    }
}