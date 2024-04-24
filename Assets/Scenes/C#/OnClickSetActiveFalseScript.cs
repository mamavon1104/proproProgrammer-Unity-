using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickSetActiveFalseScript : MonoBehaviour, IPointerClickHandler
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("a");
        gameObject.SetActive(false);
    }
}
