//using System.IO;
//using UnityEditor;
//using UnityEngine;

//namespace Mamavon.DownLoad
//{
//    [CreateAssetMenu(menuName = "Mamavon Packs/DownLoad My Packs", fileName = "DownLoadMy.asset")]
//    public class DownLoadMyPacksMamavon : ScriptableObject
//    {
//        /// <summary>
//        /// �A�Z�b�g�̃p�X
//        /// </summary>
//        public string selectAssetPath;

//        // Method to open a folder panel and set the ASSET_PATH
//        public void SetAssetPath()
//        {
//            string path = EditorUtility.OpenFolderPanel("Select Folder", "", "");
//            if (!string.IsNullOrEmpty(path))
//            {
//                selectAssetPath = path;
//                EditorUtility.SetDirty(this);
//                AssetDatabase.SaveAssets();
//            }
//        }

//        public void SetDefaultPath()
//        {
//            selectAssetPath = "C:\\Users\\vanntann\\Desktop\\ProjectBase\\Assets\\Scenes\\mamavon";
//        }

//        public void DownloadAllFiles(string parentDirectoryPath)
//        {
//            if (string.IsNullOrEmpty(selectAssetPath))
//            {
//                Debug.LogError("�A�Z�b�g�p�X����ł��B�_�E�����[�h�ł��܂���B");
//                return;
//            }

//            // �A�Z�b�g�p�X����t�@�C�����擾
//            string[] files = Directory.GetDirectories(selectAssetPath);

//            string myFilesStr = "";
//            // �t�@�C�����R�s�[���ĕۑ�
//            foreach (string file in files)
//            {
//                // �t�@�C�����̂ݎ擾
//                string fileName = Path.GetFileName(file);

//                // �t�@�C���̕ۑ���p�X
//                string destinationPath = Path.Combine(parentDirectoryPath, fileName);

//                myFilesStr = $"\n{destinationPath}\n{file}\n";

//                CopyFolder(file, destinationPath);
//            }

//            AssetDatabase.Refresh();
//            myFilesStr.Debuglog();
//            "�_�E�����[�h���������܂���".Debuglog(TextColor.Yellow);
//        }
//        private void CopyFolder(string sourceFolder, string destinationFolder)
//        {
//            // �R�s�[��̃t�H���_�����݂��Ȃ��ꍇ�͍쐬����
//            if (!Directory.Exists(destinationFolder))
//            {
//                Directory.CreateDirectory(destinationFolder);
//            }

//            // �t�@�C�����R�s�[����
//            string[] files = Directory.GetFiles(sourceFolder);
//            foreach (string file in files)
//            {
//                string fileName = Path.GetFileName(file);
//                string destFile = Path.Combine(destinationFolder, fileName);
//                File.Copy(file, destFile, true);
//            }

//            // �T�u�t�H���_���ċA�I�ɃR�s�[����
//            string[] subfolders = Directory.GetDirectories(sourceFolder);
//            foreach (string subfolder in subfolders)
//            {
//                string folderName = Path.GetFileName(subfolder);
//                string destFolder = Path.Combine(destinationFolder, folderName);
//                CopyFolder(subfolder, destFolder);
//            }
//        }
//    }

//    [CustomEditor(typeof(DownLoadMyPacksMamavon))]
//    public class SamplePathScriptableObjectInspector : Editor
//    {
//        private DownLoadMyPacksMamavon myScript;
//        private string parentDirectory;
//        private string parentDirectoryPath;

//        private void OnEnable()
//        {
//            myScript = (DownLoadMyPacksMamavon)target;
//            UpdatePaths();
//        }

//        /// <summary>
//        /// path���擾���鏈���B
//        /// </summary>
//        private void UpdatePaths()
//        {
//            string thisObjAssetPath = AssetDatabase.GetAssetPath(myScript); // Assets/Scenes/mamavon/MyScriptableObjs/DownLoadMy.asset
//            string directory = Path.GetDirectoryName(thisObjAssetPath);     // Assets/Scenes/mamavon/MyScriptableObjs
//            parentDirectory = Directory.GetParent(directory).Name;          // MyscriptableObjs�̐e��mamavon
//            parentDirectoryPath = Path.Combine(directory, "..");            // Assets/Scenes/mamavon/-MyScriptableObjs-/.. 
//        }

//        public override void OnInspectorGUI()
//        {
//            // �x�[�X��Inspector GUI��`��
//            base.OnInspectorGUI();

//            // �w���v�{�b�N�X��\��
//            if (parentDirectory == "mamavon")
//            {
//                EditorGUILayout.HelpBox("�e�t�H���_�̖��O�́umamavon�v�ł��B", MessageType.Info);
//            }
//            else
//            {
//                EditorGUILayout.HelpBox($"�e�t�H���_�̖��O���u{parentDirectory}�v�ł��B\n�umamavon�v�ɐݒ肵�Ȃ����Ă��������B", MessageType.Error);
//            }

//            // �p�X�̐ݒ�{�^��
//            if (GUILayout.Button("�p�X�̐ݒ������"))
//            {
//                myScript.SetAssetPath();
//                UpdatePaths(); // �p�X���X�V
//            }

//            // �f�t�H���g�p�X�ݒ�{�^��
//            if (GUILayout.Button("�f�t�H���g�p�X�ɐݒ肷��"))
//            {
//                myScript.SetDefaultPath();
//                UpdatePaths(); // �p�X���X�V
//            }

//            // �e�f�B���N�g�����umamavon�v�łȂ��ꍇ�͑������^�[��
//            if (parentDirectory != "mamavon")
//                return;

//            // �_�E�����[�h�{�^��
//            if (GUILayout.Button("mamavon�x�[�X����ŐV�ł��_�E�����[�h"))
//            {
//                myScript.DownloadAllFiles(parentDirectoryPath);
//            }
//        }
//    }
//}