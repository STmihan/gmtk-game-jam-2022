using System;
using Music;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Transform _window;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _quitButton;

        private void Awake()
        {
            _resumeButton.onClick.AddListener(() =>
            {
                TogglePause(false);
            });
            _quitButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(1);
            });
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel")) TogglePause(!_window.gameObject.activeInHierarchy);
        }

        private void TogglePause(bool b)
        {
            EventSystem.current.SetSelectedGameObject(_resumeButton.gameObject);
            _window.gameObject.SetActive(b);
            Time.timeScale = b ? 0 : 1;
        }
    }
}