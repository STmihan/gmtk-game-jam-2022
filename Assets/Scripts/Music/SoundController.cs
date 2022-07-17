using UnityEngine;
using UnityEngine.Events;

namespace Music
{
    public class SoundController : MonoBehaviour
    {
        public static SoundController Instance;
        public AudioSource Music;
        public AudioSource Sounds;
        public AudioClip InterfaceClick;
        
        private void Awake()
        {
            if (Instance == null) Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static void Play(AudioClip clip) => Instance.Sounds.PlayOneShot(clip);

        public static void PlayInterface()
        {
            Instance.Sounds.PlayOneShot(Instance.InterfaceClick);
        }

        public static void Mute(bool b)
        {
            Instance.Music.mute = b;
            Instance.Sounds.mute = b;
        }
    }
}