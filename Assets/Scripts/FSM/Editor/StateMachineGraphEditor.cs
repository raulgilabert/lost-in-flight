using System;
using UnityEditor;
using UnityEngine;

namespace FSM.Editor
{
    [CustomEditor(typeof(StateMachineGraph))]
    public class StateMachineGraphEditor : UnityEditor.Editor
    {
        public override bool HasPreviewGUI() => true;

        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            var stateMachineGraph = target as StateMachineGraph;
            
            if (stateMachineGraph is null) return;

            foreach (var state in stateMachineGraph.states)
            {
                EditorGUILayout.LabelField($"State: {state.name}", EditorStyles.boldLabel);
                
                EditorGUI.indentLevel++;
                foreach (var behaviour in state.behaviours)
                {
                    if (behaviour is null) continue;
                    
                    var label = behaviour.GetLabel();
                    if (String.IsNullOrEmpty(label)) label = ObjectNames.NicifyVariableName(behaviour.GetType().Name);
                    
                    EditorGUILayout.LabelField(label);
                }

                if (state.behaviours.Length == 0)
                {
                    EditorGUILayout.LabelField("This state does nothing");
                }
                EditorGUI.indentLevel--;
            }
        }
    }
}