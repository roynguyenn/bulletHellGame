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

	private SpawnerManager _spawnerManager;

	public int CurrentHunger => _currentHunger;
	public int Level => _currentMaxHungerIndex + 1;
	public int MaxLevel => stages.Count;

	private void Start()
	{
		_spawnerManager = FindObjectOfType<SpawnerManager>();
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
				// TODO: Handle leveling up logic
				_spawnerManager.IncreaseDifficulty(Level);
			}
			else
			{
				_currentHunger = stages[_currentMaxHungerIndex - 1];
				// TODO: Handle winning logic
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
			// TODO: Handle losing logic
		}
		UpdateBars();
	}

	private void UpdateBars()
	{
		float fillAmount = (float)_currentHunger / stages[_currentMaxHungerIndex];
		fullBar.fillAmount = fillAmount;
	}
}