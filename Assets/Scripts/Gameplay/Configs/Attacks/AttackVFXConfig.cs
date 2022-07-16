using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Configs.Attacks
{
    [CreateAssetMenu]
    public class AttackVFXConfig : SerializedScriptableObject
    {
        public List<Attack> AttackVFXs = new();
    }
}