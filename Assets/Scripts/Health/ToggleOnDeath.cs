using System;
using UnityEngine;

namespace Health
{
    public class ToggleOnDeath : MonoBehaviour
    {
        enum Action
        {
            Activate,
            Deactivate
        }

        enum Target
        {
            Self,
            Parent,
            Children
        }

        [Serializable]
        struct ToggleAction
        {
            public MonoBehaviour component;
            public Action mode;
        }
        
        [SerializeField] private ToggleAction[] toggleActions;

        private void Start()
        {
            GetComponent<Health>().onDeath.AddListener(OnDeath);
        }

        private void OnDeath()
        {
            foreach (var action in toggleActions)
            {
                if (action.component == null) continue;
                
                action.component.enabled = action.mode == Action.Activate;
            }
        }
    }
}