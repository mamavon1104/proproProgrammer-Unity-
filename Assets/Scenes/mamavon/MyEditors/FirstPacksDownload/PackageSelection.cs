//using UnityEngine;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//namespace Mamavon.Setting
//{
//#if UNITY_EDITOR
//    /// <summary>
//    /// �p�b�P�[�W�I��p�� ScriptableObject����B
//    /// </summary>
//    [CreateAssetMenu(fileName = "PackageSelection", menuName = "Mamavon Packs/Packages Select", order = 1)]
//    public class PackageSelection : ScriptableObject
//    {
//        public UnityPackages selectedPacks;
//        public void SetEverythingPackages() => selectedPacks = UnityPackages.Everything;
//        public void ResetPackages() => selectedPacks = UnityPackages.None;
//    }

//    [CustomEditor(typeof(PackageSelection))]  //typeof����requireComponent�Ɠ��������Ŏg����݂�����
//    public class PackageSelectionEditor : Editor
//    {
//        private UnityPackages[] allPackages;

//        private void OnEnable()
//        {
//            // ��x���� Enum �̑S�l���擾���ăL���b�V��
//            allPackages = (UnityPackages[])System.Enum.GetValues(typeof(UnityPackages));
//        }

//        public override void OnInspectorGUI()
//        {
//            PackageSelection myScript = (PackageSelection)target;
//            DrawDefaultInspector();

//            GUILayout.Space(10);
//            EditorGUILayout.LabelField("�ȉ��A�I������Ă���p�b�P�[�W:");
//            GUILayout.Space(10);

//            DisplayPackageGroup(myScript, "Code Packs", UnityPackages.CodePacks);
//            DisplayPackageGroup(myScript, "Unity Tool Packs", UnityPackages.UnityToolPacks);
//            DisplayPackageGroup(myScript, "Unity Visual Packs", UnityPackages.UnityVisualPacks);
//            DisplayPackageGroup(myScript, "Others", UnityPackages.Others);

//            GUILayout.Space(20);

//            if (GUILayout.Button("�I�񂾃p�b�P�[�W���C���X�g�[�������Ⴂ�܂���B"))
//            {
//                PackageDownloadClass.DownloadPackage(myScript, PackageManagerAction.Install);
//            }
//            if (GUILayout.Button("�I�񂾃p�b�P�[�W���A���C���X�g�[�������Ⴂ�܂���B"))
//            {
//                PackageDownloadClass.DownloadPackage(myScript, PackageManagerAction.Uninstall);
//            }

//            GUILayout.Space(10);

//            if (GUILayout.Button("�S�đI�����܂��傤�B"))
//            {
//                myScript.SetEverythingPackages();
//            }
//            if (GUILayout.Button("���Z�b�g���܂��B"))
//            {
//                myScript.ResetPackages();
//            }
//        }

//        /// <summary>
//        /// �`�F�b�N�{�b�N�X�Ƃ��̕`����s���Ă��܂�
//        /// </summary>
//        /// <param name="myScript">����ScriptableObject</param>
//        /// <param name="groupName">�O���[�v�̖��O�A�Ⴆ��CodePacks(rx,task,container)�Ƃ�</param>
//        /// <param name="groupFlag">name�ƈ�v����悤��UnityPackages�̓z�A�v�f�����Z(A | B)����Ă�����̂������ɓ���</param>
//        private void DisplayPackageGroup(PackageSelection myScript, string groupName, UnityPackages groupFlag)
//        {
//            bool isGroupSelected = (myScript.selectedPacks & groupFlag) == groupFlag;

//            EditorGUI.BeginChangeCheck();
//            GUILayout.Space(10);
//            isGroupSelected = EditorGUILayout.Toggle(groupName, isGroupSelected);

//            if (EditorGUI.EndChangeCheck())
//            {
//                if (isGroupSelected)
//                {
//                    myScript.selectedPacks |= groupFlag;
//                }
//                else
//                {
//                    myScript.selectedPacks &= ~groupFlag;
//                }
//            }

//            foreach (UnityPackages package in allPackages)
//            {
//                if (package == UnityPackages.None || package == UnityPackages.Everything || package == groupFlag)
//                    continue;

//                if ((groupFlag & package) != package)
//                    continue;

//                GUILayout.BeginHorizontal();
//                GUILayout.Space(20);

//                bool isSelected = (myScript.selectedPacks & package) == package;

//                EditorGUI.BeginChangeCheck();
//                isSelected = EditorGUILayout.Toggle(package.ToString(), isSelected);

//                if (EditorGUI.EndChangeCheck())
//                {
//                    if (isSelected)
//                    {
//                        myScript.selectedPacks |= package;
//                    }
//                    else
//                    {
//                        myScript.selectedPacks &= ~package;
//                    }
//                }
//                GUILayout.EndHorizontal();
//            }
//        }
//    }
//#endif
//}
