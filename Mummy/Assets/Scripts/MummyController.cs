using UnityEngine;
using System.Collections;

public class MummyController : MonoBehaviour {

	public Joystick moveJoyStick;
	public bool gyroOn = false;

	float forwardSpeed = 4.0f;
	float backwardSpeed = 1.0f;
	float sidestepSpeed = 1.0f;
	Vector2 rotationSpeed = new Vector2(50, 25);

	private Transform thisTransform;
	private CharacterController character;
	private Vector3 velocity;
//	private Animation animation;

	// Use this for initialization
	void Start ()
	{
		// Cache component lookup at startup instead of doing this every frame
		thisTransform = GetComponent<Transform>();
		character = GetComponent<CharacterController>();
//		velocity = character.velocity;
//		animation = GetComponent<Animation> ();
//		animation.Play ("Idle_02");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (gyroOn)
		{
			return;
		}

		Vector3 movement = thisTransform.TransformDirection(new Vector3(moveJoyStick.position.x, 0, moveJoyStick.position.y));
		if (moveJoyStick.position.x == 0 && moveJoyStick.position.y == 0)
		{
			animation.Play ("Idle_02");
			return;
		}
		movement.y = 0;
		movement.Normalize ();

		// Apply movement from move joystick
		Vector2 absJoyPos = new Vector2(Mathf.Abs(moveJoyStick.position.x), Mathf.Abs(moveJoyStick.position.y));

		if(absJoyPos.y >= absJoyPos.x)
		{
			if(moveJoyStick.position.y > 0)
			{
				movement *= forwardSpeed * absJoyPos.y;
				animation.Play ("Run");
			} else
			{
				movement *= backwardSpeed * absJoyPos.y;
				animation.Rewind("Walk");
			}
		} else
		{
			movement *= sidestepSpeed * absJoyPos.x;
		}

//		movement += velocity;

		movement *= 10.0f * Time.deltaTime;

		character.Move(movement);
	}
}
