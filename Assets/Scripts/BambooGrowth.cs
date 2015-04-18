using UnityEngine;
using System.Collections;

public class BambooGrowth : MonoBehaviour
{
	[SerializeField]
	Transform nextAnchor;

	const float startYScale = 0.1f;
	const float growthTimeSec = 0.01f;
	const float growthRate = 1f / growthTimeSec;

	public int Index { get;  set; }

	public GrowStalk Stalk { get; set; }

	public void StartGrowth()
	{
		transform.localScale = new Vector3(1f, startYScale, 1f);

		StartCoroutine(Grow());
	}

	IEnumerator Grow()
	{
		while (transform.localScale.y < 1)
		{
			transform.localScale += new Vector3(0f, growthRate, 0f) * Time.deltaTime;
			yield return null;
		}

		transform.localScale = new Vector3(1f, 1f, 1f);

		if (Index < Stalk.SectionCount)
			GrowNewSegment();
	}

	void GrowNewSegment()
	{
		var nextSegment = Instantiate(gameObject, nextAnchor.position, transform.rotation * Stalk.Lean) as GameObject;

		nextSegment.name = "Segment" + (Index + 1);
		nextSegment.transform.parent = transform;

		var growth = nextSegment.GetComponent<BambooGrowth>();
		growth.Index = Index + 1;
		growth.Stalk = Stalk;

		growth.StartGrowth();
	}
}
