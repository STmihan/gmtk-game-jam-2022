using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Configs.Enemies
{
    [Serializable]
    public abstract class Enemy
    {
        [PreviewField(ObjectFieldAlignment.Center)]
        public GameObject Prefab;
        public int MaxHp;
        public float MoveSpeed;
        public float RotationDuration;
        public float DelayBetweenHits;
        public float Range;
        public string Type => GetType().ToString();
    }
}