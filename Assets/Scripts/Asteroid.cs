using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	private Quaternion originalRotation;
	private float currentRotateAngle;

	private void Start()
	{
		originalRotation = transform.rotation;
	}
	private void FixedUpdate()
	{
		RotateAsteroid();
	}
	private void RotateAsteroid()
	{
		currentRotateAngle++;
		var rotationY = Quaternion.AngleAxis(currentRotateAngle, Vector3.up);
		var rotationX = Quaternion.AngleAxis(currentRotateAngle, Vector3.right);
		transform.rotation = originalRotation * rotationY * rotationX;
	}
}
