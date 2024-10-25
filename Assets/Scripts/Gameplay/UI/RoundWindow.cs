using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class RoundWindow : MonoBehaviour
    {
        
        [SerializeField] private TextMeshProUGUI roundText;
        [SerializeField] private CanvasGroup canvasGroup;

        public event Action AnimationEnded;
        
        private Tween _tween;

        public void ShowAndCloseAnimation(int num, Action OnComplete = null)
        {
            RenderText(num);
            
            DOTween.Sequence()
                .Append(Show())
                .AppendInterval(0.5f)
                .Append(Close())
                .AppendCallback(() =>
                {
                    OnComplete?.Invoke();
                    AnimationEnded?.Invoke();
                    
                });
        }

        public Tween Show()
        {
            _tween?.Kill();
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            _tween = canvasGroup.DOFade(1f,0.5f);
            return _tween;
        }

        public Tween Close()
        {
            _tween?.Kill();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            _tween = canvasGroup.DOFade(0f,0.5f);
            return _tween;
        }

        private void RenderText(int num)
        {
            roundText.text = $"Round {num.ToString()}";
        }
    }
}
