using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRemoveAssignmentToPram : MonoBehaviour
{
	private void Start()
	{
		Debug.Log("代金" + 60);
		Debug.Log("割引代金:" + Discount(60, 30, 8000));
	}

	private int Discount(int inputPrice, int quantity, int yearToDate)
	{
		if (inputPrice > 50)
		{
			inputPrice -= 5;
		}

		if (quantity > 100)
		{
			inputPrice -= 2;
		}

		if (yearToDate > 10000)
		{
			inputPrice -= 5;
		}

		return inputPrice;
	}
}
