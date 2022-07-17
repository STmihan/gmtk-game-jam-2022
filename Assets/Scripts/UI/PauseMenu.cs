using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Transform _window;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private Slider _soundSlider;
        
        private void Awake()
        {
            _resumeButton.onClick.AddListener(() =>
            {
                TogglePause(false);
            });
            _quitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
            _soundSlider.onValueChanged.AddListener(arg0 =>
            {
                
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