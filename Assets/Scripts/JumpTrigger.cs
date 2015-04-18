using UnityEngine;
using System.Collections;

public class JumpTrigger : MonoBehaviour
{
	public PlayerMovement toNotify;

	int collisionCount = 0;

	void OnTriggerEnter(Collider other)
	{
		if (collisionCount == 0)
			toNotify.JumpEnter();

		collisionCount++;
	}

	void OnTriggerExit()
	{
		collisionCount--;

		if (collisionCount == 0)
			toNotify.JumpExit();
	}
}
