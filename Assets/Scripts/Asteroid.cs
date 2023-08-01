using UnityEngine;

public class Asteroid : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (!other.TryGetComponent<PlayerHealth>(out var playerHealth))
			return;
		
		playerHealth.Crush();
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
