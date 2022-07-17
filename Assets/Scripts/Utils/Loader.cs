using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class Loader : MonoBehaviour
    {
        private void Awake()
        {
            SceneManager.LoadScene(1);
        }

    }
}