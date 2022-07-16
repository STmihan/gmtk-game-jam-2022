using UnityEngine;

namespace Utils
{
    public static class UnityUtils
    {
        public static bool Equal(this LayerMask layerMask, int layer)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}