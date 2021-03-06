using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
	public static event Action OnBoost;
	public static event Action OnDie;

	[SerializeField] private Rigidbody spaceship;
	[SerializeField] private SpaceshipConfig spaceshipConfig;
	[SerializeField] private SmoothFollow mainCamera;

	private float moveForwardSpeed;
	private float moveSidewaysSpeed;
	private const float StartBoostFilling = 3f;
	private bool readyForSpeedUp;
	private bool readyForFillBoost;
	private bool isAlive;

	public static float BoostFilling { get; private set; } = 3f;
	private void Awake()
	{
		readyForSpeedUp = true;
		readyForFillBoost = true;
	}
	private void OnEnable()
	{
		BeforeTheStartView.OnGameStart += ChangeIsAliveState;
	}
	private void OnDisable()
	{
		BeforeTheStartView.OnGameStart -= ChangeIsAliveState;
	}

	private void Start()
	{
		moveForwardSpeed = spaceshipConfig.MoveForwardSpeed;
		moveSidewaysSpeed = spaceshipConfig.MoveSidewaysSpeed;
	}
	private void FixedUpdate()
	{
		MoveForward();
		FeelBoost();

	}
	private void Update()
	{
		SetSpeedBoost();
	}
	private void MoveForward()
	{
		if (!isAlive) return;
		var inputHorizontal = Input.GetAxis("Horizontal");
		var direction = new Vector3(inputHorizontal, 0, 0);
		spaceship.velocity = Vector3.forward * moveForwardSpeed + direction * moveSidewaysSpeed;
		TiltTheSpaceship(inputHorizontal);	
	}
	private void TiltTheSpaceship(float inputX)
	{
		//limiting the movement of the ship to the sides
		var clampedPositionX = Mathf.Clamp(transform.position.x, -spaceshipConfig.MoveSidewaysLimit, spaceshipConfig.MoveSidewaysLimit);
		var currentTransform = transform;
		var currentPosition = transform.position;
		currentPosition = new Vector3(clampedPositionX, currentPosition.y, currentPosition.z);
		currentTransform.position = currentPosition;
		transform.rotation = Quaternion.Euler(0, 0, -inputX * spaceshipConfig.RotateAngleCoefficient);
	}
	private void SetSpeedBoost()
	{
		if (!Input.GetKeyDown(KeyCode.Space)) return;
		if (!readyForSpeedUp || !isAlive) return;
		StartCoroutine(SetAcceleration(spaceshipConfig.AcceletarionTime, spaceshipConfig.AccelerationCoefficient));

	}
	private IEnumerator SetAcceleration(float time, float coefficient)
	{
		moveForwardSpeed *= coefficient;
		AudioManager.Instance.PlayEffect(spaceshipConfig.AccelerationSound);
		mainCamera.IsZoomBoosted = true;
		readyForSpeedUp = false;
		readyForFillBoost = false;
		yield return new WaitForSeconds(time);
		moveForwardSpeed /= coefficient;
		mainCamera.IsZoomBoosted = false;
		readyForFillBoost = true;
	}
	private void FeelBoost()
	{
		if (!isAlive) return;
		OnBoost?.Invoke();

		if (!readyForFillBoost)
		{
			BoostFilling -= Time.deltaTime;
		}
		else if (readyForFillBoost && BoostFilling < StartBoostFilling)
		{
			BoostFilling += Time.deltaTime;
			if (BoostFilling >= StartBoostFilling)
			{
				readyForSpeedUp = true;
			}
		}
	}
	private void ChangeIsAliveState(bool state)
	{
		isAlive = state;
	}
	private void OnCollisionEnter(Collision collision)
	{
		isAlive = false;
		spaceship.velocity = Vector3.zero;
		AudioManager.Instance.PlayEffect(spaceshipConfig.DieSound);
		Instantiate(spaceshipConfig.DieParticle, transform.position, Quaternion.identity);
		gameObject.SetActive(false);
		OnDie?.Invoke();
	}
}
