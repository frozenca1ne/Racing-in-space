using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
	[SerializeField] private Rigidbody spaceship;
	[SerializeField] private SpaceshipConfig spaceshipConfig;

	private float moveForwardSpeed;
	private float moveSidewaysSpeed;
	private float moveSidewaysLimit;
	private float rotateAngleCoefficient;


	private void Start()
	{
		moveForwardSpeed = spaceshipConfig.MoveForwardSpeed;
		moveSidewaysSpeed = spaceshipConfig.MoveSidewaysSpeed;
		moveSidewaysLimit = spaceshipConfig.MoveSidewaysLimit;
		rotateAngleCoefficient = spaceshipConfig.RotateAngleCoefficient;
	}
	private void FixedUpdate()
	{
		MoveForward();
	}

	private void MoveForward()
	{
		var inputHorizontal = Input.GetAxis("Horizontal");
		var direction = new Vector3(inputHorizontal, 0, 0);
		spaceship.velocity = Vector3.forward * moveForwardSpeed + direction * moveSidewaysSpeed;
		TiltTheSpaceship(inputHorizontal);
	}
	private void TiltTheSpaceship(float inputX)
	{
		var clampedPositionX = Mathf.Clamp(transform.position.x, -moveSidewaysLimit, moveSidewaysLimit);
		var currentTransform = transform;
		var currentPosition = transform.position;
		currentPosition = new Vector3(clampedPositionX, currentPosition.y, currentPosition.z);
		currentTransform.position = currentPosition;
		transform.rotation = Quaternion.Euler(0, 0, -inputX * rotateAngleCoefficient);
	}


}