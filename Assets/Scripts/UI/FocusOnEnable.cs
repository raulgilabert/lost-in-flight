using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class FocusOnEnable : MonoBehaviour
    {
        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}