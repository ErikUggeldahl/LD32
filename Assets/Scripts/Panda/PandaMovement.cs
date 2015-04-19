using UnityEngine;
using System.Collections;

public class PandaMovement : MonoBehaviour
{
	[SerializeField]
	Animator animator;

	[SerializeField]
	PandaAnimation pandaAnimation;

	Transform runningFrom;

	const float safeDistance = 100f;
	const float maxSpeed = 10f;

	public void StartRunning(Transform awayFrom)
	{
		runningFrom = awayFrom;

		StartCoroutine(Run());
	}

	IEnumerator Run()
	{
		animator.SetBool("Running", true);

		var rb = GetComponent<Rigidbody>();

		var distance = float.NegativeInfinity;

		while (distance < safeDistance)
		{
			var runDirection = transform.position - runningFrom.position;
			distance = runDirection.magnitude;
			runDirection.Normalize();

			RaycastHit hit;
			Physics.Raycast(transform.position, Vector3.down, out hit, float.PositiveInfinity, LayerMask.NameToLayer("Ground"));

			var rotation = Quaternion.LookRotation(runDirection).eulerAngles;
			rotation.x = 0;
			transform.rotation = Quaternion.Euler(rotation);

			var force = transform.forward * 5f * Time.fixedDeltaTime;
			if (rb.velocity.magnitude + force.magnitude < maxSpeed)
				rb.AddForce(force, ForceMode.VelocityChange);

			yield return new WaitForFixedUpdate();
		}

		animator.SetBool("Running", false);
	}
}
