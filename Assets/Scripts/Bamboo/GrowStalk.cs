using UnityEngine;
using System.Collections;

public class GrowStalk : MonoBehaviour
{
	const int minSections = 5;
	const int maxSections = 20;

	int sectionCount;
	public int SectionCount { get { return sectionCount; } }

	const float maxLean = 0.05f;

	Quaternion lean;
	public Quaternion Lean { get { return lean; } }

	[SerializeField]
	GameObject bambooGO;

	bool canPierce = true;
	public bool CanPierce { get { return canPierce; } }

	void Start()
	{
		sectionCount = Random.Range(minSections, maxSections);

		var leanRand = Random.insideUnitCircle * maxLean;
		lean = Quaternion.LookRotation(Vector3.forward, Vector3.up + new Vector3(leanRand.x, 1f, leanRand.y));

		var bamboo = Instantiate(bambooGO, transform.position, Quaternion.identity) as GameObject;

		bamboo.name = "Segment0";
		bamboo.transform.parent = transform;

		var growth = bamboo.GetComponent<BambooGrowth>();
		growth.Stalk = this;
		growth.StartGrowth();
	}

	public void FinishGrowing()
	{
		canPierce = false;
	}
}
