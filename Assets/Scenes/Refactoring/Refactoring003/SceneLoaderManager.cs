using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    [SerializeField] string nextSceneName = "Stage02";
    public void OnTapNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
    public void OnTapRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
