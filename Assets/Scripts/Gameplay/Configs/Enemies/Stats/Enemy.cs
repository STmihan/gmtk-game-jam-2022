using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Configs.Enemies.Stats
{
    [Serializable]
    public abstract class Enemy
    {
        [PreviewField(ObjectFieldAlignment.Center)]
        public CharacterView Prefab;
        public int MaxHp;
        public float MoveSpeed;
        public float RotationDuration;
        public float DelayBetweenHits;
        public float Range;
        [ShowInInspector]
        public Type Type => GetType();
    }
}