using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
	float moveSpeed = 5f;

	void Update()
	{
		Move();
	}

	void Move()
	{
		Vector3 movementVector = Vector3.zero;

		if (Input.GetKey(KeyCode.W)) movementVector += Vector3.up;
		if (Input.GetKey(KeyCode.S)) movementVector += Vector3.down;
		if (Input.GetKey(KeyCode.D)) movementVector += Vector3.right;
		if (Input.GetKey(KeyCode.A)) movementVector += Vector3.left;

		movementVector.Normalize();
		transform.position += movementVector * moveSpeed * Time.deltaTime;
	}
}