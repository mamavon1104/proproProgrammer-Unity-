using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Mamavon.Funcs.Scriptables
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
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(TextDebugCS))] //typeof����requireComponent�Ɠ��������Ŏg����݂�����
    public class TextDebugEditor : Editor
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
