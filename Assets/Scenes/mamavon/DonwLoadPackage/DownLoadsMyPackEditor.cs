//#if UNITY_EDITOR
//using UnityEditor;
//using UnityEditor.PackageManager;
//using UnityEditor.PackageManager.Requests;
//using UnityEngine;

//namespace Mamavon.DownLoad
//{
//    /// <summary>
//    /// �p�b�P�[�W�I��p�� ScriptableObject����B
//    /// </summary>

//    public class DownLoadsMyPackEditor : EditorWindow
//    {
//        [MenuItem("Mamavon/MyPacksDownload")]
//        public static void ShowWindow()
//        {
//            GetWindow(typeof(DownLoadsMyPackEditor));
//        }
//        private void OnEnable()
//        {

//        }

//        private void OnGUI()
//        {
//            EditorGUILayout.LabelField("�ȉ��A�I������Ă���p�b�P�[�W:");

//            if (GUILayout.Button("�I�񂾃p�b�P�[�W���C���X�g�[�������Ⴂ�܂���B"))
//            {
//                DownLoadMyPacks();
//            }
//        }
//        private void DownLoadMyPacks()
//        {
//            const string MY_PACKS_NAME = "C:\\Users\\vanntann\\Desktop\\ProjectBase\\Assets\\Scenes\\mamavon"; //Public���|�W�g���ɂ��Ȃ��Ⴂ���Ȃ��B
//            AddRequest addRequest = Client.Add(MY_PACKS_NAME);
//            Debug.Log($"{MY_PACKS_NAME}���C���X�g�[�����܁`��");
//            EditorApplication.update += Progress;
//            void Progress()
//            {
//                if (addRequest.IsCompleted)
//                {
//                    if (addRequest.Status == StatusCode.Success)
//                        Debug.Log("�C���X�g�[�����������܂���: " + addRequest.Result.packageId);
//                    else if (addRequest.Status >= StatusCode.Failure)
//                        Debug.LogError("�C���X�g�[���Ɏ��s���܂���: " + addRequest.Error.message);
//                    EditorApplication.update -= Progress;
//                    AssetDatabase.Refresh();
//                }
//            }
//        }
//    }
//}
//#endif