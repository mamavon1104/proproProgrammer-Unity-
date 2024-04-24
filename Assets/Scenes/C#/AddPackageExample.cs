using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Mamavon.Setting
{
    public class AddPackageExample : MonoBehaviour
    {
        static List<AddRequest> Requests = new List<AddRequest>();

        [MenuItem("FirstSetting/Add Package")]
        static void Add()
        {
            List<string> packageIds = new List<string>
            {
                "https://github.com/neuecc/UniRx.git?path=Assets/Plugins/UniRx/Scripts",
                "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask",
            };

            foreach (string packageId in packageIds)
            {
                AddRequest request = Client.Add(packageId);
                Requests.Add(request);
            }

            EditorApplication.update += Progress;
        }

        static void Progress()
        {
            for (int i = 0; i < Requests.Count; i++)
            {
                AddRequest request = Requests[i];

                if (request.IsCompleted)
                {
                    if (request.Status == StatusCode.Success)
                        Debug.Log("インストール: " + request.Result.packageId);
                    else if (request.Status >= StatusCode.Failure)
                        Debug.Log(request.Error.message);

                    Requests.RemoveAt(i);
                    i--;
                }
            }

            if (Requests.Count == 0)
            {
                EditorApplication.update -= Progress;
            }
        }
    }
}