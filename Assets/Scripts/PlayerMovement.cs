using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float forceMagnitude = 150f;
	[SerializeField] private float maxVelocity = 6f;
	[SerializeField] private float rotationSpeed = 30f;

	private Camera _mainCamera;
	private Rigidbody _playerRigidbody;
	private Vector3 _movementDirection;

	private void Awake()
	{
		_mainCamera = Camera.main;
		_playerRigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		ProcessInput();
		KeepPlayerOnScreen();
		RotateToFaceVelocity();
	}

	private void FixedUpdate()
	{
		if (_movementDirection == Vector3.zero)
			return;
		
		_playerRigidbody.AddForce(_movementDirection * (forceMagnitude * Time.fixedDeltaTime), ForceMode.Force);
		_playerRigidbody.velocity = Vector3.ClampMagnitude(_playerRigidbody.velocity, maxVelocity);
	}

	private void ProcessInput()
	{
		if (Touch.activeTouches.Count > 0)
		{
			var worldPoint = _mainCamera.ScreenToWorldPoint(Touch.activeTouches[0].screenPosition);

			_movementDirection = transform.position - worldPoint;
			_movementDirection.z = 0;
			_movementDirection.Normalize();
		}
		else
		{
			_movementDirection = Vector3.zero;
		}
	}

	private void KeepPlayerOnScreen()
	{
		Vector3 newPosition = transform.position;

		var viewportPoint = _mainCamera.WorldToViewportPoint(transform.position);
		if (viewportPoint.x > 1f)
		{
			newPosition.x = -newPosition.x + 0.1f;
		}
		else if (viewportPoint.x < 0f)
		{
			newPosition.x = -newPosition.x - 0.1f;
		}
		
		if (viewportPoint.y > 1f)
		{
			newPosition.y = -newPosition.y + 0.1f;
		}
		else if (viewportPoint.y < 0f)
		{
			newPosition.y = -newPosition.y - 0.1f;
		}
		
		transform.position = newPosition;
	}

	private void RotateToFaceVelocity()
	{
		if (_playerRigidbody.velocity == Vector3.zero)
			return;
		
		var targetRotation = Quaternion.LookRotation(_playerRigidbody.velocity, Vector3.back);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

	}
}
