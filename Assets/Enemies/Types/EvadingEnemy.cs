using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadingEnemy : Enemy
{
	public override string Name => "Evading Enemy";

	public static float CloseDistance { get; set; } = 2.5f;
	public float Speed { get; set; } = 5f;

	private Transform _playerTransform;
	private Vector3 _targetPosition;
	private bool _movingAway;

	void Start()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		_playerTransform = player.transform;
		PickNewTargetPosition();
	}

	void Update()
	{
		float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
		if (distanceToPlayer < CloseDistance)
		{
			MoveAwayFromPlayer();
		}
		else
		{
			MoveToTarget();
		}
	}

	void MoveToTarget()
	{
		if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
		{
			PickNewTargetPosition();
		}
		else
		{
			Vector3 direction = (_targetPosition - transform.position).normalized;
			transform.position += direction * Speed * Time.deltaTime;
		}
	}

	void MoveAwayFromPlayer()
	{
		Vector3 awayDirection = (transform.position - _playerTransform.position).normalized;
		_targetPosition = transform.position + awayDirection * CloseDistance;
		_movingAway = true;
		MoveToTarget();
	}

	void PickNewTargetPosition()
	{
		if (!_movingAway)
		{
			_targetPosition = new Vector3(Random.Range(-7f, 7f) + _playerTransform.position.x, Random.Range(-5f, 5f) + _playerTransform.position.y, 0f);
		}
		_movingAway = false;
	}
}