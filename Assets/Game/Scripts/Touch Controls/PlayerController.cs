using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float speed = 5.0f;
	public float movementThreshold = 0.5f;

	public float minimumSwipeDistance = 20.0f;
	public float swipeTimeThreshold = 0.5f;

	public Image container;

	public GameObject projectile;

	private Rigidbody2D _rigidbody2D;
	private Joystick joystick;

	private Vector2 downPosition;
	private float downTime;

	private Vector2 upPosition;
	private float upTime;

	void Start()
	{
		joystick = container.GetComponent<Joystick>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		JoystickInput();
		SwipeInput();
	}

	private void JoystickInput()
	{
		if (joystick.inputDirection.magnitude > movementThreshold)
		{
			_rigidbody2D.velocity = new Vector2(joystick.inputDirection.x * speed, joystick.inputDirection.y * speed);
		}
		else
		{
			_rigidbody2D.velocity = Vector2.zero;
		}
	}

	private void SwipeInput()
	{
		if(Input.touchCount > 0)
		{
			Touch touch = Input.touches[0];

			if (touch.phase == TouchPhase.Began)
			{
				downPosition = touch.position;
				upPosition = touch.position;

				downTime = Time.time;
			}

			if (touch.phase == TouchPhase.Ended)
			{
				upPosition = touch.position;
				Vector2 swipeDirection = (upPosition - downPosition).normalized;
				float swipeDistance = (upPosition - downPosition).magnitude;

				upTime = Time.time;

				float deltaTime = upTime - downTime;

				if (swipeDistance > minimumSwipeDistance && deltaTime < swipeTimeThreshold)
				{
					GameObject fireball = Instantiate(projectile, transform.position, Quaternion.identity);
					fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(swipeDirection.x * speed, swipeDirection.y * speed);
				}
			}
		}
	}

}
