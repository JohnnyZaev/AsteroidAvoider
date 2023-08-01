using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
	[SerializeField] private GameObject gameOverDisplay;
	[SerializeField] private AsteroidSpawner asteroidSpawner;
	
	public void EndGame()
	{
		asteroidSpawner.enabled = false;
		gameOverDisplay.SetActive(true);
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
