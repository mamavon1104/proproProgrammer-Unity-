using System.Collections.Generic;
using UnityEngine;

public class TestMethodExtraction : MonoBehaviour
{
    [SerializeField] private string m_name = "TestName";
    [SerializeField] private int m_amount = 10;

    private void Start()
    {
        List<int> testIntList = new List<int> { 2, 1, 5, 1, 5 };
        PrintList(testIntList);
        PrintMembers();
    }

    private void PrintList(List<int> testIntList)
    {
        foreach (var value in testIntList)
        {
            Debug.Log("List Value:" + value);
        }
    }
    private void PrintMembers()
    {
        Debug.Log("name :" + m_name);
        Debug.Log("amount :" + m_amount);
    }
}
