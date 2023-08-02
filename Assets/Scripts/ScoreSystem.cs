using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
	[SerializeField] private TMP_Text scoreText;
	[SerializeField] private float scoreMultiplier;

	private float _score;
	private bool _isGameOver;

	private void Update()
	{
		if (_isGameOver)
			return;
		_score += Time.deltaTime * scoreMultiplier;
		scoreText.text = Mathf.FloorToInt(_score).ToString();
	}

	public int StopScore()
	{
		_isGameOver = true;
		scoreText.text = string.Empty;
		return Mathf.FloorToInt(_score);
	}
}
