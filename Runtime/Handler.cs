using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneLinker/Handler", menuName = "SceneLinker/Handler")]
public class Handler : ScriptableObject
{
    public SceneAsset sceneToHandle;
    public List<Linker> linkers = new List<Linker>();

    public static void AddLinker(Handler handler)
    {
        var linkName = Linker.GetDefaultLinkerName(handler);
        var linker = Linker.CreateNewLinker(linkName);

        handler.linkers.Add(linker);
        Linker.CreateLinkerSubasset(linker, handler);
    }

    public static void RemoveLinker(Handler handler, Linker linker)
    {
        handler.linkers.Remove(linker);
        Linker.RemoveLinkerSubasset(linker);
    }
}