using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Mamavon.Funcs.Scriptables
{
    /// <summary>
    /// �p�b�P�[�W�I��p�� ScriptableObject����B
    /// </summary>
    [CreateAssetMenu(fileName = "DebugVectorObjs", menuName = "Mamavon Packs/ScriptableObject/Debug ScripObjs/Vector Debug")]
    public class VectorScripObjs : ScriptableObject
    {
        public Vector3 vector3_A, vector3_B;
        public Vector2 vector2_A, vector2_B;
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(VectorScripObjs))] //typeof����requireComponent�Ɠ��������Ŏg����݂�����
    public class VectorScripObjsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            VectorScripObjs myScript = (VectorScripObjs)target;
            DrawDefaultInspector();

            GUILayout.Space(10);

            if (GUILayout.Button("Vector3���m�̊|���Z���o�͂��܂�"))
            {
                myScript.vector3_A.MultiVecs(myScript.vector3_B).Debuglog(TextColor.Blue);
            }
            if (GUILayout.Button("Vector2���m�̊|���Z���o�͂��܂�"))
            {
                myScript.vector2_A.MultiVecs(myScript.vector2_B).Debuglog(TextColor.Green);
            }
        }
    }
#endif
}
