using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Configs
{
    [CreateAssetMenu]
    public class PrefabsConfig : ScriptableObject
    {
        [PreviewField]
        public GameObject PlayerPrefab;
    }
}