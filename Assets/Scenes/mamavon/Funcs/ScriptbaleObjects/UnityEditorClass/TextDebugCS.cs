using UnityEngine;
using Mamavon.Funcs;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Mamavon.Data
{
    /// <summary>
    /// パッケージ選択用の ScriptableObjectだよ。
    /// </summary>
    [CreateAssetMenu(fileName = "DebugTextObjs", menuName = "Mamavon Packs/ScriptableObject/Debug ScripObjs/Text Debug")]
    public class TextDebugCS : ScriptableObject
    {
        public TextColor textColor;
        public string text;

        public void DebugText()
        {
            //値が返ってくるので配列に値を追加しながらもちゃんとDebug出来ますよ。
            var a = text.Debuglog(textColor);
        }

        [ContextMenu("全ての色でDebugLog")]
        private void DebugAllColor()
        {
            TextColor[] colors = (TextColor[])Enum.GetValues(typeof(TextColor));
            foreach (var color in colors)
            {
                text.Debuglog($"Color = {color}", color);
            }
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(TextDebugCS))] //typeofってrequireComponentと同じ感じで使えるみたいね
    public class TextDebugEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            TextDebugCS myScript = (TextDebugCS)target;
            DrawDefaultInspector();

            GUILayout.Space(10);

            if (GUILayout.Button("一回デバッグします。"))
            {
                myScript.DebugText();
            }
        }
    }
#endif
}
