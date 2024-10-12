using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform player; // Assign the player in the inspector
	public Vector3 offset; // Set a desired offset in the inspector

	void LateUpdate()
	{
		if (player != null)
		{
			transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
		}
	}
}