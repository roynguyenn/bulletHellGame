using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
	public override string Name => "Basic Enemy";
    
	public float Speed { get; set; } = 2.0f;
	
	private Transform _playerTransform;
	private float _existenceTime = 0f;

	void Start()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null)
		{
			_playerTransform = player.transform;
		}
	}

	void Update()
	{
		if (_playerTransform != null)
		{
			Vector3 direction = (_playerTransform.position - transform.position).normalized;
			transform.position += direction * Speed * Time.deltaTime;
		}

		_existenceTime += Time.deltaTime;
		if (_existenceTime >= 5f)
		{
			Remove();
		}
	}
}