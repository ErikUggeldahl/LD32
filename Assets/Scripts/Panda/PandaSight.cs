using UnityEngine;
using System.Collections;

public class PandaSight : MonoBehaviour
{

	[SerializeField]
	PandaMovement movement;

	void OnTriggerEnter(Collider other)
	{
		movement.StartRunning(other.transform);
	}
}
