using UnityEditor;
using UnityEngine;

namespace HNK.Tools.Editor {
    [CustomEditor(typeof(GameEventFactory))]

    public class WISGameEventEditor : UnityEditor.Editor {

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            var factory = (GameEventFactory) target;
            factory.GenerateTemplateTextAssets();

            if (string.IsNullOrEmpty(factory.eventName)) return;
            if (string.IsNullOrEmpty(factory.scriptLocation)) return;
            if (!AssetDatabase.IsValidFolder(factory.scriptLocation)) return;

            if (GUILayout.Button("Generate Event Scripts")) {
                factory.GenerateEventScripts();
                factory.ClearData();
            }
        }
    }
}