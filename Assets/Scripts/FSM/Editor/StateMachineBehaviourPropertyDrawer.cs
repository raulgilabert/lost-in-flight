using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FSM.Editor
{
    [CustomPropertyDrawer(typeof(StateMachineBehaviour))]
    public class StateMachineBehaviourPropertyDrawer : PropertyDrawer
    {
        private static readonly Type[] BehaviourTypes;
        private static readonly GUIContent[] BehaviourTypeNames;

        static StateMachineBehaviourPropertyDrawer()
        {
            BehaviourTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => typeof(StateMachineBehaviour).IsAssignableFrom(t) && !t.IsAbstract).ToArray();
            
            BehaviourTypeNames = BehaviourTypes.Select(t => new GUIContent(ObjectNames.NicifyVariableName(t.Name))).ToArray();   
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float lineHeight = EditorGUIUtility.singleLineHeight;
            float currentY = position.y;
            
            if (property.managedReferenceValue != null)
            {
                var behaviour = (StateMachineBehaviour)property.managedReferenceValue;
                var newLabel = behaviour.GetLabel();
                label = String.IsNullOrEmpty(newLabel)
                    ? new GUIContent(ObjectNames.NicifyVariableName(behaviour.GetType().Name))
                    : new GUIContent(newLabel);
            }
            
            EditorGUI.BeginProperty(position, GUIContent.none, property);
            
            Rect labelRect = new Rect(position.x, currentY, position.width, lineHeight);
            EditorGUI.LabelField(labelRect, label);
            currentY += lineHeight;
            
            int currentIndex = Array.IndexOf(BehaviourTypes, property.managedReferenceValue?.GetType());

            Rect popupRect = new Rect(position.x, currentY, position.width, lineHeight);
            int newIndex = EditorGUI.Popup(popupRect, new GUIContent("Script"), currentIndex, BehaviourTypeNames);

            if (newIndex != currentIndex && newIndex >= 0)
            {
                property.managedReferenceValue = Activator.CreateInstance(BehaviourTypes[newIndex]);
            }

            if (property.managedReferenceValue != null)
            {
                Rect behaviourRect = new Rect(position.x, currentY, position.width, position.height - 2 * lineHeight);
                EditorGUI.PropertyField(behaviourRect, property, GUIContent.none, true);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.managedReferenceValue != null)
            {
                return EditorGUI.GetPropertyHeight(property, true) + EditorGUIUtility.singleLineHeight;
            }

            return 2 * EditorGUIUtility.singleLineHeight;
        }
    }
}