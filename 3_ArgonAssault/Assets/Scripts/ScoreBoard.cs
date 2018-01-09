using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
	int score;
	Text scoreText;

	// Use this for initialization
	void Start ()
	{
		scoreText = GetComponent<Text>();
		scoreText.text = score.ToString();
	}

	public void IncreaseScore(int score)
	{
		this.score += score;
		scoreText.text = this.score.ToString();
	}
}
