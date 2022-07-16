using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Configs.Attacks
{
    [CreateAssetMenu]
    public class AttackConfig : SerializedScriptableObject
    {
        public List<Attack> Attacks = new();
    }
}