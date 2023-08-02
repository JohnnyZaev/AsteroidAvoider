using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private TMP_Text gameOverText;
	[SerializeField] private Button continueButton;
	[SerializeField] private ScoreSystem scoreSystem;
	[SerializeField] private GameObject gameOverDisplay;
	[SerializeField] private AsteroidSpawner asteroidSpawner;
	
	public void EndGame()
	{
		asteroidSpawner.enabled = false;
		gameOverDisplay.SetActive(true);
		gameOverText.text = $"Your score is: {scoreSystem.StopScore()}";
	}
	
	public void PlayAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void ContinueButton()
	{
		AdManager.Instance.ShowAdd(this);

		continueButton.interactable = false;

	}

	public void ReturnToMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void ContinueGame()
	{
		scoreSystem.StartTimer();
		player.transform.position = Vector3.zero;
		player.SetActive(true);
		player.GetComponent<Rigidbody>().velocity = Vector3.zero;
		asteroidSpawner.enabled = true;
		gameOverDisplay.SetActive(false);
		
	}
}
