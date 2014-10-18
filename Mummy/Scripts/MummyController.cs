using UnityEngine;
using System.Collections;

// This script must be attached to a GameObject that has a CharacterController

public class MummyController : MonoBehaviour {

	public Joystick moveJoystick;

	public Transform cameraPivot;						// The transform used for camera rotation
	public Transform cameraTransform;					// The actual transform of the camera

	public float speed = 0.25;
	public Vector2 rotationSpeed = new Vector2(50, 25); // Camera rotation speed for each axis

	private Transform thisTransform;
	private CharacterController character;
	private Vector3 velocity;

	// Use this for initialization
	void Start () {
		// Cache component lookup at startup instead of doing this every frame
		thisTransform = GetComponent( Transform );
		character = GetComponent( CharacterController );

		// Move the character to the correct start position in the level, of one exists
		GameObject spwan = GameObject.Find( "PlayerSpawn" );
		if( spawn )
		{
			thisTransform.position = spawn.transform.position;
		}
	}
	
	void FaceMovementDirection()
	{
		Vector3 horizontalVelocity = character.velocity;
		horizontalVelocity.y = 0; // Ignore vertical movement

		// If moving significantly in a new direction, point that character in that direction
		if( horizontalVelocity.magnitude > 0.1 )
		{
			thisTransform.forward = horizontalVelocity.normalized;
		}
	}

	void OnEndGame()
	{
		// Disable joystick when the game ends
		moveJoystick.Disable();

		// Don't allow any more control changes when the game ends
		this.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		Vector3 movement = cameraTransform.TransformDirection( Vector3( moveJoystick.position.x, 0, moveJoystick.position.y ) );
		// We only want the camera-space horizontal direction
		movement.y = 0;
		movement.Normalize(); // Adjust mangitude after ignoring vertical movement

		// Let's use the largest component of the joystick position for the speed
		Vector2 absJoyPos = new Vector2( Mathf.Abs( moveJoystick.position.x ). Mathf.Abs( moveJoystick.position.y ) );
		movement *= speed * ( ( absJoyPos.x > absJoyPos.y ) ? absJoyPos.x : absJoyPos.y );

		movement += velocity;
		movement *= Time.deltaTime;

		// Actually move the character
		character.Move( movement );

		// Face the character to match with where she is moving
		FaceMovementDirection();
	}
}
