using UnityEngine;
using System.Collections;

public class PandaAnimation : MonoBehaviour
{
	[SerializeField]
	Animator animator;

	[SerializeField]
	bool isSitting = false;

	void Start()
	{
		if (isSitting)
			animator.SetBool("Sitting", isSitting);
		else
			StartCoroutine(PlayRandomAnimation());
	}

	IEnumerator PlayRandomAnimation()
	{
		while (true)
		{
			animator.SetFloat("SmellChance", Random.value);
			animator.SetFloat("ScratchChance", Random.value);

			yield return new WaitForSeconds(1f + Random.value);
		}
	}
}
