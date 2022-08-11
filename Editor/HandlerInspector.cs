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

    [MenuItem("Assets/Create/Scene Linker/Handler of this Scene", false, 1)]
    public static void CreateHandlerFromSelection(MenuCommand command)
    {
        var selection = Selection.GetFiltered<SceneAsset>(SelectionMode.Assets);
        if (selection.Length == 0)
        {
            Debug.LogError("You must select a scene in order to use this function");
            return;
        }
        foreach (var scene in selection)
        {
            var newHandler = CreateInstance<Handler>();
            newHandler.sceneToHandle = scene;
            newHandler.name = $"{scene.name}Handler";

            var assetPath = AssetDatabase.GetAssetPath(scene).Replace($"{scene.name}.unity", $"{newHandler.name}.asset");
            AssetDatabase.CreateAsset(newHandler, assetPath);
        }

        AssetDatabase.SaveAssets();
    }
}