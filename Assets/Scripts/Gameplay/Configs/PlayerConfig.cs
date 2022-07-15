using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Configs
{
    [CreateAssetMenu]
    public class PlayerConfig : ScriptableObject
    {
        [PreviewField]
        public GameObject PlayerPrefab;
        [Header("Player")]
        public float PlayerMovementSpeed;
        public float PlayerRotationDuration;
        public float PlayerAttackSpeed;
        public float PlayerProjectileSpeed;
    }
}