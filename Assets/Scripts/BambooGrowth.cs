using UnityEngine;
using System.Collections;

public class BambooGrowth : MonoBehaviour
{
	void Start()
	{
		StartCoroutine(Grow());
	}

	void Update()
	{
	}

	IEnumerator Grow()
	{
		while (transform.localScale.y < 10)
		{
			transform.localScale += new Vector3(0f, 1f, 0f) * Time.deltaTime;
			yield return null;
		}
	}
}
