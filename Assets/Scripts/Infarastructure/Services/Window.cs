using DG.Tweening;
using UnityEngine;

namespace Infarastructure.Services
{
    public class Window : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        private Tween _tween;
        
        public virtual void Show()
        {
            _tween?.Kill();
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            _tween = canvasGroup.DOFade(1f,0.5f);
        }

        public void Hide()
        {
            _tween?.Kill();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            _tween = canvasGroup.DOFade(0f,0.5f);
        }
    }
}