using Gameplay.UnityComponents;
using UnityEngine;

namespace Gameplay.Configs
{
    [CreateAssetMenu]
    public class TilesConfig : ScriptableObject
    {
        public TileView[] Tiles;
    }
}