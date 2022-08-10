using UnityEditor;

[CustomEditor(typeof(Link))]
public class LinkInspector : Editor
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