using UnityEngine;
using Mamavon.Funcs;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Mamavon.Data
{
    /// <summary>
    /// �p�b�P�[�W�I��p�� ScriptableObject����B
    /// </summary>
    [CreateAssetMenu(fileName = "DebugTextObjs", menuName = "Mamavon Packs/ScriptableObject/Debug ScripObjs/Text Debug")]
    public class TextDebugCS : ScriptableObject
    {
        public TextColor textColor;
        public string text;

        public void DebugText()
        {
            //�l���Ԃ��Ă���̂Ŕz��ɒl��ǉ����Ȃ����������Debug�o���܂���B
            var a = text.Debuglog(textColor);
        }

        [ContextMenu("�S�Ă̐F��DebugLog")]
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
    [CustomEditor(typeof(TextDebugCS))] //typeof����requireComponent�Ɠ��������Ŏg����݂�����
    public class TextDebugEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            TextDebugCS myScript = (TextDebugCS)target;
            DrawDefaultInspector();

            GUILayout.Space(10);

            if (GUILayout.Button("���f�o�b�O���܂��B"))
            {
                myScript.DebugText();
            }
        }
    }
#endif
}
