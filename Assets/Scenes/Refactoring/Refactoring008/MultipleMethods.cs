using System.Collections;
using UnityEngine;

public class MultipleMethod : MonoBehaviour
{
    [SerializeField] private GameObject scoreObj;
    private void Start()
    {
        StartCoroutine(ActivateScoreObject());
    }
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        if (scoreObj.activeInHierarchy)
            StartCoroutine(DeActivateScoreObject());
        else
            StartCoroutine(ActivateScoreObject());
    }
    IEnumerator ActivateScoreObject()
    {
        yield return new WaitForSeconds(0.1f);
        scoreObj.SetActive(true);
    }
    IEnumerator DeActivateScoreObject()
    {
        yield return new WaitForSeconds(0.1f);
        scoreObj.SetActive(false);
    }
}