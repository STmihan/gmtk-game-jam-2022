using System;

namespace Gameplay.Components.Enemy
{
    public struct OnEnemyDieEvent
    {
        public Type Type;
        public int Count;
    }
}