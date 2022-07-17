using System;
using UnityEngine;

namespace Gameplay.UnityComponents
{
    public class VFXView : MonoBehaviour
    {
        public GameObject VFX;

        private void Awake()
        {
            Destroy(gameObject, 3f);
        }
    }
}