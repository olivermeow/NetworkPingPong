using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infarastructure.Services
{
    public enum GameResult
    {
        Win = 1,
        Lose = 2
    }
    public class ResultWindow : Window
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI resultText;

        private Dictionary<GameResult, string> _resultTextMap = new Dictionary<GameResult, string>()
        {
            {GameResult.Lose, "You Lose!"},
            {GameResult.Win, "You Win!"}
        };
        
        public event Action Clicked;

        private void Awake()
        {
            button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked() => Clicked?.Invoke();

        public void ShowWin()
        {
            ShowResult(GameResult.Win);
        }

        public void ShowLose()
        {
            ShowResult(GameResult.Lose);
        }

        public void ShowResult(GameResult gameResult)
        {
            if (_resultTextMap.TryGetValue(gameResult, out string value))
            {
                resultText.text = value;
            }
            Show();
        }
    }
}