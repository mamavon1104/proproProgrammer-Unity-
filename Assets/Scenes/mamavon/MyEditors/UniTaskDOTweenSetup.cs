using Mamavon.Funcs;
using UnityEditor;

namespace Mamavon.Editor
{
    public static class UniTaskDOTweenSetup
    {
        //�ύX���Ȃ��łˁB
        const string ADD_SUPPORT = "UNITASK_DOTWEEN_SUPPORT";

        [MenuItem("Mamavon/UniTask DOTween Setup ")]
        public static void SetupUniTask()
        {
            AddDOTweenSupport();
            "DOTween�T�|�[�g���������܂����B".Debuglog("���s�I���I");  //�g��������C�ɂ��Ȃ���
        }

        static void AddDOTweenSupport()
        {
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup
                 (EditorUserBuildSettings.selectedBuildTargetGroup);

            if (!defines.Contains(ADD_SUPPORT)) //Dic�̂悤��Contains�ŒT�m
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(
                    EditorUserBuildSettings.selectedBuildTargetGroup,
                    (defines + ";" + ADD_SUPPORT).Trim(';').Debuglog("Trim���ꂽ�l�I")
                );
            }
        }
    }
}