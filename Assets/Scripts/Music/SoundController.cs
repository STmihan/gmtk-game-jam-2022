using UnityEngine;

namespace Music
{
    public class SoundController : MonoBehaviour
    {
        public static SoundController Instance;
        public AudioSource Music;
        public AudioSource Sounds;
        
        private void Awake()
        {
            if (Instance == null) Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static void Play(AudioClip clip) => Instance.Sounds.PlayOneShot(clip);
    }
}