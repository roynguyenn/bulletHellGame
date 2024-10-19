using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerTester : MonoBehaviour
{
	public HungerBar hungerBar;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			hungerBar.IncreaseHunger(10);
			Debug.Log("G");
		}

		if (Input.GetKeyDown(KeyCode.H))
		{
			hungerBar.DecreaseHunger(10);
		}
	}
}