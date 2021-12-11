using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Spaceship", fileName = "Spaceship")]
public class SpaceshipConfig : ScriptableObject
{
	[Header("Move")]
	[SerializeField] private float moveForwardSpeed = 10f;
	[SerializeField] private float moveSidewaysSpeed = 10f;
	[Header("Tilt")]
	[SerializeField] private float moveSidewaysLimit = 2.5f;
	[SerializeField] private float rotateAngleCoefficient = 10f;
	[Header("Acceleration")]
	[SerializeField] private float accelerationCoefficient = 2f;
	[SerializeField] private float accelerationTime = 3f;
	[SerializeField] private AudioClip accelerationSound;
	[Header("Die")]
	[SerializeField] private GameObject dieParticle;
	[SerializeField] private AudioClip dieSound;

	public float MoveForwardSpeed => moveForwardSpeed;
	public float MoveSidewaysSpeed => moveSidewaysSpeed;
	public float MoveSidewaysLimit => moveSidewaysLimit;
	public float RotateAngleCoefficient => rotateAngleCoefficient;
	public float AccelerationCoefficient => accelerationCoefficient;
	public float AcceletarionTime => accelerationTime;
	public AudioClip AccelerationSound => accelerationSound;
	public GameObject DieParticle => dieParticle;
	public AudioClip DieSound => dieSound;
}
