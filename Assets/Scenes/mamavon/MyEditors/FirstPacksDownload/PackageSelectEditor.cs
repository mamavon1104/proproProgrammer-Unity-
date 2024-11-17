
#if UNITY_EDITOR
using Mamavon.Funcs;
using UnityEditor;
using UnityEngine;

namespace Mamavon.MyEditor
{
    /// <summary>
    /// �p�b�P�[�W�I��p�� ScriptableObject����B
    /// </summary>

    public class PackageSelectionEditorWindow : EditorWindow
    {
        private Vector2 scrollPosition = Vector2.zero; //scrollPos��vector2�ŕۑ�

        private UnityPackages selectedPacks;
        public UnityPackages SelectedPacks { get => selectedPacks; }

        public void SetEverythingPackages() => selectedPacks = UnityPackages.Everything;
        public void ResetPackages() => selectedPacks = UnityPackages.None;


        [MenuItem("Mamavon/My Editors/DownLoadPacksWindow")]
        public static void ShowWindow()
        {
            GetWindow(typeof(PackageSelectionEditorWindow));
        }

        private UnityPackages[] allPackages;

        private void OnEnable()
        {
            // ��x���� Enum �̑S�l���擾���ăL���b�V��
            allPackages = (UnityPackages[])System.Enum.GetValues(typeof(UnityPackages));
        }

        void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(
                scrollPosition, //�X�N���[���|�W�V�����A
                false,�@//���͓����Ȃ��悤�ɁA
                true,   //�c�͓�����悤�ɁA
                GUILayout.ExpandHeight(true) //GUIlayout(�ϒ�����)��true(auto)�ɐݒ肷��B
            );

            GUILayout.Space(10);
            EditorGUILayout.LabelField("�ȉ��A�I������Ă���p�b�P�[�W:");
            GUILayout.Space(10);

            DisplayPackageGroup("Code Packs", UnityPackages.CodePacks);
            DisplayPackageGroup("Unity Tool Packs", UnityPackages.UnityToolPacks);
            DisplayPackageGroup("Unity Visual Packs", UnityPackages.UnityVisualPacks);
            DisplayPackageGroup("Others", UnityPackages.Others);

            GUILayout.Space(20);

            if (GUILayout.Button("�I�񂾃p�b�P�[�W���C���X�g�[�������Ⴂ�܂���B"))
            {
                PackageDownloadClass.DownloadPackage(this, PackageManagerAction.Install);
            }
            if (GUILayout.Button("�I�񂾃p�b�P�[�W���A���C���X�g�[�������Ⴂ�܂���B"))
            {
                PackageDownloadClass.DownloadPackage(this, PackageManagerAction.Uninstall);
            }

            GUILayout.Space(10);

            if (GUILayout.Button("�S�đI�����܂��傤�B"))
            {
                SetEverythingPackages();
            }
            if (GUILayout.Button("���Z�b�g���܂��B"))
            {
                ResetPackages();
            }

            GUILayout.EndScrollView();
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�Ƃ��̕`����s���Ă��܂�
        /// </summary>
        /// <param name="myScript">����ScriptableObject</param>
        /// <param name="groupName">�O���[�v�̖��O�A�Ⴆ��CodePacks(rx,task,container)�Ƃ�</param>
        /// <param name="groupFlag">name�ƈ�v����悤��UnityPackages�̓z�A�v�f�����Z(A | B)����Ă�����̂������ɓ���</param>
        private void DisplayPackageGroup(string groupName, UnityPackages groupFlag)
        {
            bool isGroupSelected = (selectedPacks & groupFlag) == groupFlag;

            EditorGUI.BeginChangeCheck();
            GUILayout.Space(10);
            isGroupSelected = EditorGUILayout.Toggle(groupName, isGroupSelected);

            if (EditorGUI.EndChangeCheck())
            {
                if (isGroupSelected)
                {
                    selectedPacks |= groupFlag;
                }
                else
                {
                    selectedPacks &= ~groupFlag;
                }
            }

            foreach (UnityPackages package in allPackages)
            {
                if (package == UnityPackages.None || package == UnityPackages.Everything || package == groupFlag)
                    continue;

                if ((groupFlag & package) != package)
                    continue;

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);

                bool isSelected = (selectedPacks & package) == package;

                EditorGUI.BeginChangeCheck();
                isSelected = EditorGUILayout.Toggle(package.ToString(), isSelected);

                if (EditorGUI.EndChangeCheck())
                {
                    if (isSelected)
                    {
                        selectedPacks |= package;
                    }
                    else
                    {
                        selectedPacks &= ~package;
                    }
                }
                GUILayout.EndHorizontal(); // endHorizontal
            }
        }
    }
}
#endif
