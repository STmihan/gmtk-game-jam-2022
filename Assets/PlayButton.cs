using System;
using System.Collections;
using System.Collections.Generic;
using Music;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            SoundController.PlayInterface();
            SceneManager.LoadScene(2);
        });
    }
}
