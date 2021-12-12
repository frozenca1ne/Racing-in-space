using UnityEngine;

public class Asteroid : MonoBehaviour
{
	[SerializeField] private int points = 5;
	[SerializeField] private int pointsPerAsteroid = 1;

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
		//free rotation along the axes
		currentRotateAngle++;
		var rotationY = Quaternion.AngleAxis(currentRotateAngle, Vector3.up);
		var rotationX = Quaternion.AngleAxis(currentRotateAngle, Vector3.right);
		transform.rotation = originalRotation * rotationY * rotationX;
	}
	private void OnBecameInvisible()
	{
		var helper = new AsteroidScoreHelper();
		helper.CurrentAsteroidsEarn += pointsPerAsteroid;
		helper.CurrentPointsPerAsteroid += points;
		Destroy(gameObject);
	}
}
