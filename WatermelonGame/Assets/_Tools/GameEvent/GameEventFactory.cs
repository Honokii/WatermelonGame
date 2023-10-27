using System.IO;
using UnityEditor;
using UnityEngine;

namespace HNK.Tools {
    [CreateAssetMenu(menuName = "HNK/Tools/Game Event Factory", fileName = "NewGameEventFactory")]
    public class GameEventFactory : ScriptableObject {
        private const string BaseGameEventTemplateLocation = "Assets/_Tools/GameEvent/BaseEventTemplate.txt";
        private const string BaseGameEventListenerTemplateLocation = "Assets/_Tools/GameEvent/BaseEventListenerTemplate.txt";
        private const string TemplateIdEventName = "#EVENTNAME#";
        private TextAsset _baseGameEventTemplateTextAsset;
        private TextAsset _baseGameEventListenerTemplateTextAsset;
        
        public string eventName;
        public string scriptLocation;

        public void ClearData() {
            eventName = string.Empty;
            scriptLocation = string.Empty;
        }

        public void GenerateEventScripts() {
            if (!AreTemplatesReady) {
                Debug.Log("BaseGameEventHandler: GenerateEventScripts! some templates are not found.");
                return;
            }

            var baseGameEventTemplate = _baseGameEventTemplateTextAsset.text;
            var baseGameEventListenerTemplate = _baseGameEventListenerTemplateTextAsset.text;

            baseGameEventTemplate = baseGameEventTemplate.Replace(TemplateIdEventName, eventName);
            baseGameEventListenerTemplate = baseGameEventListenerTemplate.Replace(TemplateIdEventName, eventName);

            var validLoc = scriptLocation.Replace("Assets/", ""); //remove the Assets/ on the location as this is also in the Application.dataPath
            
            var eventFilePath = $"{Application.dataPath}/{validLoc}/{eventName}.cs";
            var eventListenerFilePath = $"{Application.dataPath}/{validLoc}/{eventName}Listener.cs";
            
            using (var eventWriter = new StreamWriter(eventFilePath, true)) {
                eventWriter.Write(baseGameEventTemplate);
                eventWriter.Close();
            }

            using (var eventListenerWriter = new StreamWriter(eventListenerFilePath, true)) {
                eventListenerWriter.Write(baseGameEventListenerTemplate);
                eventListenerWriter.Close();
            }
            
            AssetDatabase.Refresh();
        }

        public void GenerateTemplateTextAssets() {
            if (AreTemplatesReady) return;
            
            if (_baseGameEventTemplateTextAsset == null) {
                _baseGameEventTemplateTextAsset = AssetDatabase.LoadAssetAtPath(BaseGameEventTemplateLocation, typeof(TextAsset)) as TextAsset;
            }

            if (_baseGameEventListenerTemplateTextAsset == null) {
                _baseGameEventListenerTemplateTextAsset = AssetDatabase.LoadAssetAtPath(BaseGameEventListenerTemplateLocation, typeof(TextAsset)) as TextAsset;
            }
        }

        private bool AreTemplatesReady =>
            _baseGameEventTemplateTextAsset != null && _baseGameEventListenerTemplateTextAsset != null;
    }
}