using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TestRails : MonoBehaviour
{
    [Serializable]
    class User
    {
        public string name = "TestUser";
        public string email = "TestEmail@";
        public string password = "0123";
        public int coin = 500;
    }

    [SerializeField] User userData;

    public void CreateUser()
    {
        var json = JsonUtility.ToJson(userData);
        StartCoroutine(CreateUserPost(json));
    }

    private IEnumerator CreateUserPost(string json)
    {
        using var request = UnityWebRequest.Post("http://127.0.0.1:3000/users", json, "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("User post created successfully");
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }
}