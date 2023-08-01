using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerMovement : MonoBehaviour
{
	private Camera _mainCamera;

	private void Awake()
	{
		_mainCamera = Camera.main;
	}

	private void Update()
	{
		if (Touch.activeTouches.Count > 0)
		{
			Debug.Log(_mainCamera.ScreenToWorldPoint(Touch.activeTouches[0].screenPosition));
		}
	}
}
