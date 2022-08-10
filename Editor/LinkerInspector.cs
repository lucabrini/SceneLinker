using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(Linker))]
public class LinkerInspector : Editor
{
    private SerializedProperty _linkName;
    private SerializedProperty _destinationScene;
    private SerializedProperty _destinationLinker;

    private Linker _linker;
    private string _previousLinkerName;

    private void OnEnable()
    {
        _linker = (Linker)target;
        _linkName = serializedObject.FindProperty("linkName");
        _destinationScene = serializedObject.FindProperty("destinationScene");
        _destinationLinker = serializedObject.FindProperty("destinationLink");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_linkName);
        EditorGUILayout.PropertyField(_destinationScene);
        EditorGUILayout.PropertyField(_destinationLinker);
        serializedObject.ApplyModifiedProperties();
    }

    private static string[] GetLinkOptions(Linker linker)
    {
        if (linker.destinationScene == null)
        {
            return Array.Empty<string>();
        }

        List<string> list = new();
        list.AddRange(linker.destinationScene.linkers.Select(l => l.linkName));
        return list.ToArray();
    }
}