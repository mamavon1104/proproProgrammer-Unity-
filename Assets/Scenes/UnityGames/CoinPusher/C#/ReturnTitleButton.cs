using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnTitleButton : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.OnClickAsObservable().Subscribe(_ =>
        {
            SceneManager.LoadScene("Title");
        });
    }
}
