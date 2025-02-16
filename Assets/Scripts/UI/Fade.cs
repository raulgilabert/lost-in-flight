using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UI
{
    public class Fade : MonoBehaviour
    {
        private static readonly Color FadeInColor = Color.clear;
        private static readonly Color FadeOutColor = Color.black;
        
        public UnityEvent onFadeIn;
        public UnityEvent onFadeOut;
        
        private VisualElement _fade;

        private void OnEnable()
        {
            var uiDocument = GetComponent<UIDocument>();
            
            _fade = uiDocument.rootVisualElement.Q<VisualElement>("fade");
            
            _fade.RegisterCallback<TransitionEndEvent>(OnTransitionEnd);
        }

        private void OnDisable()
        {
            _fade.UnregisterCallback<TransitionEndEvent>(OnTransitionEnd);
        }
        
        private void OnTransitionEnd(TransitionEndEvent evt)
        {
            if (!evt.AffectsProperty("background-color")) return;

            if (_fade.style.backgroundColor == FadeInColor) onFadeIn.Invoke();
            else if (_fade.style.backgroundColor == FadeOutColor) onFadeOut.Invoke();
        }

        public void FadeIn()
        {
            _fade.style.backgroundColor = FadeInColor;
        }

        public void FadeOut()
        {
            _fade.style.backgroundColor = FadeOutColor;
        }
    }
}