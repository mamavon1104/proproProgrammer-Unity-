using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Mamavon.MyEditor
{
    public class MoveToSceneMenuItem : EditorWindow
    {
        string[] sceneNames;
        static string[] scenePaths;

        [MenuItem("Mamavon/Move To Scene")]
        public static void ShowWindows()
        {
            GetWindow<MoveToSceneMenuItem>("�V�[�����[�h�N");
        }

        private void OnEnable()
        {
            var scenes = EditorBuildSettings.scenes
                .Where(s => s.enabled)
                .ToArray();

            if (scenes.Length == 0)
            {
                return;
            }

            scenePaths = scenes.Select(s => s.path).ToArray();

            //����Ńt�@�C���̖��O���擾�ł���Ƃ̎��B
            sceneNames = scenes.Select(s => System.IO.Path.GetFileNameWithoutExtension(s.path)).ToArray();
        }

        private void OnGUI()
        {
            GUILayout.Label("�V�[�����[�h", EditorStyles.boldLabel);
            for (int i = 0; i < sceneNames.Length; i++)
            {
                string name = sceneNames[i];
                if (GUILayout.Button($"{name}�����[�h����I"))
                {
                    LoadScene(i);
                }
            }
        }

        private static void LoadScene(int sceneIndex)
        {
            string scenePath = scenePaths[sceneIndex];

            //�t�@�C������p�X�Ŏw�肵�Ă����ēǂݍ��ނƃV�[�����Ăяo����B
            if (System.IO.File.Exists(scenePath))
            {
                // isDirty�ŃV�[�����ς���Ă��邩�ǂ����𔻕ʂł���B
                // �撣���č�������̂�Dirty�Ă΂�肷��Ȃ�A�����Ă��܂����H
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    if (EditorUtility.DisplayDialog("�ۑ����܂���",
                        "���݂̕ύX��ۑ�����ꍇ�̓Z�[�u�������Ă���������",
                        "�Z�[�u", "���܂����I"))
                    {
                        EditorSceneManager.SaveOpenScenes();
                    }
                }

                // Load the new sceneObj
                EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            }
            else
            {
                Debug.LogError($"Scene file not found: {scenePath}");
            }
        }
    }

}