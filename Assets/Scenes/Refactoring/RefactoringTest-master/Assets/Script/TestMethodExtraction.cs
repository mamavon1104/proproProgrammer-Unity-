using System.Collections.Generic;
using UnityEngine;

public class TestMethodExtraction : MonoBehaviour
{
    [SerializeField] private string name = "TestName";
    [SerializeField] private int amount = 10;

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
        Debug.Log("name :" + name);
        Debug.Log("amount :" + amount);
    }
}
