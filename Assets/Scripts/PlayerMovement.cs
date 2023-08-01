using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float forceMagnitude = 50f;
	[SerializeField] private float maxVelocity = 6f;

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

	private void FixedUpdate()
	{
		if (_movementDirection == Vector3.zero)
			return;
		
		_playerRigidbody.AddForce(_movementDirection * (forceMagnitude * Time.fixedDeltaTime), ForceMode.Force);
		_playerRigidbody.velocity = Vector3.ClampMagnitude(_playerRigidbody.velocity, maxVelocity);
	}
}
