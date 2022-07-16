using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public class PlayerBlueAttack : Attack
    {
        [PreviewField]
        public VFXView Trail;
    }
}