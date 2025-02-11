using System;
using UnityEditor;
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
            public MonoScript component;
            public Action mode;
            public Target target;
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
                Type componentType = action.component.GetClass();
                Component component = action.target switch
                {
                    Target.Self => GetComponent(componentType),
                    Target.Parent => GetComponentInParent(componentType),
                    Target.Children => GetComponentInChildren(componentType),
                    _ => throw new ArgumentOutOfRangeException()
                };
                
                if (component == null) continue;
                
                ((MonoBehaviour) component).enabled = action.mode == Action.Activate;
            }
        }
    }
}