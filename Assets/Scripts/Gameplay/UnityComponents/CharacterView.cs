using UnityEngine;

namespace Gameplay.UnityComponents
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterView : MonoBehaviour
    {
        [field: SerializeField]
        public Transform FirePoint { get; private set; }
        public Animator Animator { get; private set; }
        public CharacterController CharacterController { get; private set; }

        private void Awake()
        {
            CharacterController = GetComponent<CharacterController>();
            Animator = GetComponentInChildren<Animator>();
        }
    }
}