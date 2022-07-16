﻿using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public class PlayerBrownAttack : Attack
    {
        [PreviewField]
        public VFXView HitVFX;
        [PreviewField]
        public VFXView BottomVfx;
        [PreviewField]
        public VFXView TopVFX;
    }
}