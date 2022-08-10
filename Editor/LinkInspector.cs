using SceneLinker.Runtime;
using UnityEditor;

namespace SceneLinker.Editor
{
    [CustomEditor(typeof(Link))]
    public class LinkInspector : UnityEditor.Editor
    {
        private SerializedProperty _linker;
        private Link _link;

        private void OnEnable()
        {
            _link = (Link)target;
            _linker = serializedObject.FindProperty("handledBy");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_linker);
            serializedObject.ApplyModifiedProperties();
        }
    }
}