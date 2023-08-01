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
		var position = transform.position;
		var newPosition = position;

		var viewportPoint = _mainCamera.WorldToViewportPoint(position);
		newPosition.x = viewportPoint.x switch
		{
			> 1f => -newPosition.x + 0.1f,
			< 0f => -newPosition.x - 0.1f,
			_ => newPosition.x
		};

		newPosition.y = viewportPoint.y switch
		{
			> 1f => -newPosition.y + 0.1f,
			< 0f => -newPosition.y - 0.1f,
			_ => newPosition.y
		};

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
