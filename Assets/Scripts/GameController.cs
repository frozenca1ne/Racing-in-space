using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[Header("SpawnRoad")]
	[SerializeField] private Transform player;
	[SerializeField] private RoadController roadPrefab;
	[SerializeField] private RoadController firstRoad;
	[SerializeField] private float spawnDistance = 170f;
	[Header("SpawnAsteroids")]
	[SerializeField] private GameObject asteroid;
	[SerializeField] private int startAsteroidCount = 1;
	[SerializeField] private float asteroidCreateRate = 10f;

	private RoadController newRoad;
	private readonly List<RoadController> spawnedRoads = new List<RoadController>();
	private int currentAsteroidCount;

	private void Start()
	{
		spawnedRoads.Add(firstRoad);
		currentAsteroidCount = startAsteroidCount;
		StartCoroutine(RaiseAsteroidCount(asteroidCreateRate));
	}
	private void Update()
	{
		SpawnNewRoad();
	}
	private void SpawnNewRoad()
	{
		if (player.position.z < spawnedRoads[spawnedRoads.Count - 1].EndPosition.position.z - spawnDistance ) return;
		newRoad = Instantiate(roadPrefab);
		newRoad.transform.position = spawnedRoads[spawnedRoads.Count - 1].EndPosition.position - newRoad.BeginPosition.localPosition;
		SpawnAsteroids();
		spawnedRoads.Add(newRoad);
		CheckRoadCount();
	}
	private void CheckRoadCount()
	{
		if (spawnedRoads.Count < 5) return;
		Destroy(spawnedRoads[0].gameObject);
		spawnedRoads.RemoveAt(0);
	}
	private void SpawnAsteroids()
	{
		for (var i = 0; i < currentAsteroidCount; i++)
		{
			var getPositionIndex = Random.Range(0, newRoad.AsteroidPositions.Count - 1);
			Instantiate(asteroid, newRoad.AsteroidPositions[getPositionIndex].position, Quaternion.identity);
			newRoad.AsteroidPositions.RemoveAt(getPositionIndex);
		}
	}
	private IEnumerator RaiseAsteroidCount(float spawnRate)
	{
		yield return new WaitForSeconds(1f);
		while (currentAsteroidCount < roadPrefab.AsteroidPositions.Count - 1)
		{
			yield return new WaitForSeconds(spawnRate);
			currentAsteroidCount++;
		}
	}
}
