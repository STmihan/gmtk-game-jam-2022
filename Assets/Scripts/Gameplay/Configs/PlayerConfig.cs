using UnityEngine;

namespace Gameplay.Configs
{
    [CreateAssetMenu]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Player")]
        public float PlayerMovementSpeed;
        public float PlayerRotationSpeed;
        public float PlayerAttackSpeed;
        public float PlayerProjectileSpeed;
    }
}