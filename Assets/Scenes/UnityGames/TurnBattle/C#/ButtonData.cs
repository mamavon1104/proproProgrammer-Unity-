using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class ButtonData : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_activeCanvasGroup;
    public string m_myString;
    public CanvasGroup ActiveCanvasGroup { get { return m_activeCanvasGroup; } }
}