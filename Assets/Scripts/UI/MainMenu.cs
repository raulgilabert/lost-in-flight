using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private Button _button;
        private Fade _fade;

        private void Start()
        {
            _fade = transform.parent.GetComponentInChildren<Fade>();
        }

        private void OnEnable()
        {
            var uiDocument = GetComponent<UIDocument>();
            
            _button = uiDocument.rootVisualElement.Q<Button>("play-button");

            _button.clicked += OnClick;
            _button.Focus();
        }

        private void OnDisable()
        {
            _button.clicked -= OnClick;
        }

        private void OnClick()
        {
            _fade.FadeOut();
        }
    }
}
