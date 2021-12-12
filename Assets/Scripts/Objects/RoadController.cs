using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
	[SerializeField] private Transform beginPosition;
	[SerializeField] private Transform endPosition;
	[SerializeField] private List<Transform> asteroidPositions = new List<Transform>();

	public Transform BeginPosition => beginPosition;
	public Transform EndPosition => endPosition;
	public List<Transform> AsteroidPositions => asteroidPositions;
}
