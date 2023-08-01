using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
	[SerializeField] private GameObject[] asteroidPrefabs;
	[SerializeField] private float secondsBetweenAsteroids = 1.5f;
	[SerializeField] private Vector2 forceRange;

	private float _timer;
	private Camera _mainCamera;

	private void Awake()
	{
		_mainCamera = Camera.main;
		_timer = secondsBetweenAsteroids;
	}

	private void Update()
	{
		_timer -= Time.deltaTime;

		if (_timer > 0)
			return;
		SpawnAsteroid();

		_timer = secondsBetweenAsteroids;
	}

	private void SpawnAsteroid()
	{
		var side = Random.Range(0, 4);
		
		var spawnPoint = Vector2.zero;
		var direction = Vector2.zero;

		switch (side)
		{
			case 0:
				// Left
				spawnPoint.x = 0;
				spawnPoint.y = Random.value;
				direction = new Vector2(1f, Random.Range(-1f, 1f));
				break;
			case 1:
				// Right
				spawnPoint.x = 1;
				spawnPoint.y = Random.value;
				direction = new Vector2(-1f, Random.Range(-1f, 1f));
				break;
			case 2:
				// Bottom
				spawnPoint.x = Random.value;
				spawnPoint.y = 0f;
				direction = new Vector2(Random.Range(-1f, 1f), 1f);
				break;
			case 3:
				// Top
				spawnPoint.x = Random.value;
				spawnPoint.y = 1;
				direction = new Vector2(Random.Range(-1f, 1f), -1f);
				break;
		}

		var worldSpawnPoint = _mainCamera.ViewportToWorldPoint(spawnPoint);
		worldSpawnPoint.z = 0;
		var randomAsteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
		var asteroidInstance = Instantiate(randomAsteroid, worldSpawnPoint, Quaternion.Euler(0, 0, Random.Range(0, 360f)));

		var asteroidRigidbody = asteroidInstance.GetComponent<Rigidbody>();

		asteroidRigidbody.velocity = direction.normalized * Random.Range(forceRange.x, forceRange.y);
	}
}
