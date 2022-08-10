using UnityEditor;
using UnityEngine;

namespace SceneLinker.Runtime
{
    [CreateAssetMenu(fileName = "SceneLinker/Linker", menuName = "SceneLinker/Linker")]
    public class Linker : ScriptableObject
    {
        public string linkName;
        public Handler destinationScene;
        public Linker destinationLink;

        private string _previousName;

        private Vector2 _destinationLinkLocation;

        public void SetDestinationLinkLocation(Vector2 location)
        {
            _destinationLinkLocation = location;
        }

        public Vector2 GetDestinationLinkLocation()
        {
            return _destinationLinkLocation;
        }

        private void OnValidate()
        {
            if (_previousName == null || _previousName != linkName)
            {
                //RenameLinker(linkName);
                _previousName = linkName;
            }
        }

        public static Linker CreateNewLinker(string linkName)
        {
            var linker = CreateInstance<Linker>();

            linker.name = linkName;
            linker.linkName = linkName;

            return linker;
        }

        public static string GetDefaultLinkerName(Handler parentHandler)
        {
            return $"{parentHandler.name}Linker{parentHandler.linkers.Count}";
        }

        public static void CreateLinkerSubasset(Linker linker, Handler parentHandler)
        {
            AssetDatabase.AddObjectToAsset(linker, parentHandler);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void RemoveLinkerSubasset(Linker linker)
        {
            AssetDatabase.RemoveObjectFromAsset(linker);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void RenameLinker(string newName)
        {
            name = newName;
            const string tempAssetPath = "Assets/temp-987654321.anim";
            AssetDatabase.CreateAsset(new AnimationClip(), tempAssetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            AssetDatabase.DeleteAsset(tempAssetPath);
            /*AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(this), newName);*/
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}