using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameplayUIProvider : MonoBehaviour
    {
        [HideInInspector] public int FireDiceCount;
        [HideInInspector] public int LightDiceCount;
        [HideInInspector] public int DarkDiceCount;
        [HideInInspector] public int EarthDiceCount;
        [HideInInspector] public int MaxHp;
        [HideInInspector] public int Hp;
        [HideInInspector] public int CurrentDice;
        [SerializeField] private TMP_Text _fire;
        [SerializeField] private TMP_Text _light;
        [SerializeField] private TMP_Text _dark;
        [SerializeField] private TMP_Text _earth;
        [SerializeField] private Image _hpBar;
        [Space] 
        [SerializeField] private TMP_Text _currentDiceCount;
        [SerializeField] private Image _currentDice;
        [SerializeField] private Sprite _fireDiceImage;
        [SerializeField] private Sprite _lightDiceImage;
        [SerializeField] private Sprite _darkDiceImage;
        [SerializeField] private Sprite _earthDiceImage;
        [Space] public CanvasGroup DeadScreen;
        

        private void Update()
        {
            _fire.text = FireDiceCount.ToString();
            _light.text = LightDiceCount.ToString();
            _dark.text = DarkDiceCount.ToString();
            _earth.text = EarthDiceCount.ToString();
            _hpBar.fillAmount = (float)Hp / (float)MaxHp;

            switch (CurrentDice)
            {
                case 0:
                    _currentDiceCount.text = FireDiceCount.ToString();
                    _currentDice.sprite = _fireDiceImage;
                    break;
                case 1:
                    _currentDiceCount.text = LightDiceCount.ToString();
                    _currentDice.sprite = _lightDiceImage;
                    break;
                case 2:
                    _currentDiceCount.text = DarkDiceCount.ToString();
                    _currentDice.sprite = _darkDiceImage;
                    break;
                case 3:
                    _currentDiceCount.text = EarthDiceCount.ToString();
                    _currentDice.sprite = _earthDiceImage;
                    break;
                default:
                    _currentDice.sprite = _currentDice.sprite;
                    break;
            }
        }
    }
}