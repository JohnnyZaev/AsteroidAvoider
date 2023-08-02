using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
	[SerializeField] private TMP_Text gameOverText;
	[SerializeField] private ScoreSystem scoreSystem;
	[SerializeField] private GameObject gameOverDisplay;
	[SerializeField] private AsteroidSpawner asteroidSpawner;
	
	public void EndGame()
	{
		asteroidSpawner.enabled = false;
		gameOverDisplay.SetActive(true);
		gameOverText.text += $"\n Your score is: {scoreSystem.StopScore()}";
	}
	
	public void PlayAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void ReturnToMenu()
	{
		SceneManager.LoadScene(0);
	}
}
