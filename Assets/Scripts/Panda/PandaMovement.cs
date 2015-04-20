using UnityEngine;
using System.Collections;

public class PandaMovement : MonoBehaviour
{
	[SerializeField]
	Animator animator;

	[SerializeField]
	PandaAnimation pandaAnimation;

	[SerializeField]
	AudioClip alertSound;

	Transform runningFrom;

	const float safeDistance = 80f;
	const float maxSpeed = 10f;

	public void StartRunning(Transform awayFrom)
	{
		if (runningFrom != null)
			return;

		GetComponent<AudioSource>().PlayOneShot(alertSound);

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

		runningFrom = null;
	}

	public void Die()
	{
		Debug.Log("Decreasing! " + gameObject.name);
		ScoreCount.Instance.Decrease();

		GetComponent<PandaDeathSound>().Play();

		Destroy(GetComponent<PandaAnimation>());
		Destroy(animator);
		GetComponent<Rigidbody>().isKinematic = true;
		Destroy(GetComponentInChildren<PandaSight>());
		Destroy(this);
	}
}
