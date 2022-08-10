using UnityEngine;

namespace SceneLinker.Runtime
{
    public static class Constants
    {
        public static readonly GUIStyle PaddingStyle = new GUIStyle
        {
            padding = new RectOffset(10, 10, 2, 0),
        };

        public static readonly GUIStyle CustomLinkerInspectorStyle = new GUIStyle
        {
            normal =
            {
                background = MakeTex(600, 1, new Color(0.15f, 0.15f, 0.15f))
            }
        };

        private static Texture2D MakeTex(int width, int height, Color col)
        {
            var pix = new Color[width * height];

            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;

            var result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }
    }
}