using System;
using DG.Tweening;
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