using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour 
{
	public GameObject[] fullBar;
	public GameObject[] emptyBar;
	public List<int> stages;

	private int _currentHunger = 50;
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
		_currentHunger = Mathf.Clamp(_currentHunger, 0, stages[_currentMaxHungerIndex]);
		if (_currentHunger == stages[_currentMaxHungerIndex])
		{
			_currentMaxHungerIndex++;
			if (_currentMaxHungerIndex < stages.Count)
			{
				_currentMaxHungerIndex = Mathf.Clamp(_currentMaxHungerIndex,0,stages.Count-1);
				fullBar[_currentMaxHungerIndex - 1].SetActive(false);
				
				// TODO: Handle leveling up logic
				
				
				emptyBar[_currentMaxHungerIndex - 1].SetActive(false);
				fullBar[_currentMaxHungerIndex].SetActive(true);
				emptyBar[_currentMaxHungerIndex].SetActive(true);
				clearAll();

				
				
				
				Debug.Log("DIFFICULTY INCREASED");
				Debug.Log(Level.ToString());
				_spawnerManager.IncreaseDifficulty(Level);
				
			}
			else
			{
				_currentHunger = Mathf.Clamp(_currentHunger,0 ,stages[_currentMaxHungerIndex-1]);
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
		
		_currentMaxHungerIndex = Mathf.Clamp(_currentMaxHungerIndex,0,stages.Count-1);
		float fillAmount = (float)_currentHunger / stages[_currentMaxHungerIndex];
		fullBar[_currentMaxHungerIndex].GetComponent<Image>().fillAmount = Mathf.Clamp(fillAmount,0,1);
	}   

	public void clearAll(){
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
		var bullets = GameObject.FindGameObjectsWithTag("Bullet");
		var items = GameObject.FindGameObjectsWithTag("Item");

        foreach (var enemy in enemies){
            Destroy(enemy);
        }

		foreach (var bullet in bullets){
			Destroy(bullet);
		}

		foreach (var item in items){
			Destroy(item);
		}
    }
}