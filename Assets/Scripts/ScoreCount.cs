using UnityEngine;
using System.Collections;

public class ScoreCount : MonoBehaviour
{
	[SerializeField]
	Transform pandaParent;

	[SerializeField]
	TextMesh scoreText;

	static ScoreCount instance;
	public static ScoreCount Instance { get { return instance; } }

	public delegate void VictoryFn();
	public event VictoryFn OnVictory; 

	int totalPandas;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		totalPandas = pandaParent.childCount;
		UpdateText();
	}

	public void Decrease()
	{
		totalPandas--;
		UpdateText();
	}

	void UpdateText()
	{
		if (totalPandas >= 1)
			scoreText.text = totalPandas.ToString();
		else
		{
			scoreText.text = "You win!";
			OnVictory();
		}
	}
}
