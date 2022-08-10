using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Handler))]
public class HandlerInspector : Editor
{
    private Handler _handler;
    private SerializedProperty _sceneToHandle;

    private void OnEnable()
    {
        _handler = (Handler)target;
        _sceneToHandle = serializedObject.FindProperty("sceneToHandle");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawCustomInspector();
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawCustomInspector()
    {
        EditorGUILayout.PropertyField(_sceneToHandle);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Linkers:");
        EditorGUILayout.BeginVertical(Constants.PaddingStyle);
        foreach (var linker in _handler.linkers)
        {
            DrawEmbeddedLinker(linker);
        }

        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Add Linker"))
        {
            Handler.AddLinker(_handler);
        }
    }

    private void DrawEmbeddedLinker(Linker linker)
    {
        EditorGUILayout.BeginVertical(Constants.CustomLinkerInspectorStyle);
        CreateEditor(linker).OnInspectorGUI();
        if (GUILayout.Button("Remove this"))
        {
            Handler.RemoveLinker(_handler, linker);
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Separator();
    }
}