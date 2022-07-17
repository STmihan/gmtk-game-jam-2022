using UnityEngine;

namespace Music
{
    public class MusicController : MonoBehaviour
    {
        public static MusicController Instance;
        public AudioSource Music;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            DontDestroyOnLoad(gameObject);
            Music.Play();
        }
    }
}