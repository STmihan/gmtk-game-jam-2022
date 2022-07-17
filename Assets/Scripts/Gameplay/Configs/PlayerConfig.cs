using Gameplay.UnityComponents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Configs
{
    [CreateAssetMenu]
    public class PlayerConfig : ScriptableObject
    {
        [PreviewField]
        public CharacterView PlayerPrefab;

        [Header("Player")] public int PlayerMaxHp;
        public float PlayerMovementSpeed;
        public float PlayerRotationDuration;
        public float PlayerAttackDelay;
        public float PlayerProjectileSpeed;
    }
}