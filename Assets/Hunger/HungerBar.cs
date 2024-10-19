using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour 
{
	public Image[] fullBar;
	public GameObject[] emptyBar;
	public List<int> stages;

	private int _currentHunger = 100;
	private int _currentMaxHungerIndex = 0;

	private SpawnerManager _spawnerManager;

	public int CurrentHunger => _currentHunger;
	public int Level => _currentMaxHungerIndex + 1;
	public int MaxLevel => stages.Count;

	private void Start()
	{
		
		_spawnerManager = FindObjectOfType<SpawnerManager>();
		_currentHunger = 50;
		UpdateBars();
		
	}



	public void IncreaseHunger(int amount)
	{
		_currentHunger += amount;
		if (_currentHunger > stages[_currentMaxHungerIndex])
		{
			_currentMaxHungerIndex++;
			if (_currentMaxHungerIndex < stages.Count )
			{
				_currentHunger = stages[_currentMaxHungerIndex - 1] + (_currentHunger - stages[_currentMaxHungerIndex - 1]);
				// TODO: Handle leveling up logic
				Debug.Log(_currentMaxHungerIndex.ToString());
				Debug.Log("Index before");

				fullBar[_currentMaxHungerIndex - 1].enabled = false;
				emptyBar[_currentMaxHungerIndex - 1].SetActive(false);
				fullBar[_currentMaxHungerIndex].enabled = true;
				emptyBar[_currentMaxHungerIndex].SetActive(true);

				

				_spawnerManager.IncreaseDifficulty(Level);
			}
			else
			{
				_currentHunger = Mathf.Clamp(_currentHunger,0 ,stages[_currentMaxHungerIndex - 1]);
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
			// from royce: Logic already handled in another script outside this one.
		}
		UpdateBars();
	}

	private void UpdateBars()
	{
		Debug.Log(_currentMaxHungerIndex.ToString());
		Debug.Log("Index after filling bar");

		float fillAmount = (float)_currentHunger / stages[_currentMaxHungerIndex];
		fullBar[_currentMaxHungerIndex].fillAmount = Mathf.Clamp(fillAmount,0,1);
	}
}