using System;
using System.Collections;
using System.Collections.Generic;
using Music;
using UnityEngine;
using UnityEngine.UI;

public class CustomToggle : MonoBehaviour
{
    public Sprite On;
    public Sprite Off;
    private Toggle _component;

    private void Awake()
    {
        _component = GetComponent<Toggle>();
        _component.onValueChanged.AddListener(Call);
    }

    private void Call(bool b)
    {
        _component.image.sprite = b ? On : Off;
        SoundController.Mute(b);
    }
}
