using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	public abstract string Name { get; }

	public void Remove()
	{
		gameObject.SetActive(false);
	}
}