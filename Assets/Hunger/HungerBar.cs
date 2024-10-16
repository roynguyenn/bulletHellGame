using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour 
{
	public Image fullBar;
	public GameObject emptyBar;
	public List<int> stages;

	private int _currentHunger = 20;
	private int _currentMaxHungerIndex = 0;

	private void Start()
	{
		_currentHunger = stages[_currentMaxHungerIndex];
		UpdateBars();
	}

	public void IncreaseHunger(int amount)
	{
		_currentHunger += amount;
		if (_currentHunger > stages[_currentMaxHungerIndex])
		{
			_currentMaxHungerIndex++;
			if (_currentMaxHungerIndex < stages.Count)
			{
				_currentHunger = stages[_currentMaxHungerIndex - 1] + (_currentHunger - stages[_currentMaxHungerIndex - 1]);
			}
			else
			{
				_currentHunger = stages[_currentMaxHungerIndex - 1];
			}
		}
		UpdateBars();
	}

	public void DecreaseHunger(int amount)
	{
		_currentHunger -= amount;
		if (_currentHunger <= 0)
		{
			_currentHunger = 0;
		}
		UpdateBars();
	}

	private void UpdateBars()
	{
		float fillAmount = (float)_currentHunger / stages[_currentMaxHungerIndex];
		fullBar.fillAmount = fillAmount;
	}
}