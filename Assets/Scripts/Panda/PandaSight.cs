using UnityEngine;
using System.Collections;

public class PandaSight : MonoBehaviour
{

	[SerializeField]
	PandaMovement movement;

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Found player: " + other.gameObject.name);

		movement.StartRunning(other.transform);
	}
}
