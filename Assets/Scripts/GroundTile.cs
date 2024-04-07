using UnityEngine;

public class GroundTile : MonoBehaviour
{
	private GroundSpawner _GroundSpawner;

	[SerializeField]
	private GameObject _ObstaclePrefab;

	[SerializeField]
	private GameObject _TallObstaclePrefab;

	[SerializeField]
	private float _TallObstacleChance = 0.3f;

	[SerializeField]
	private GameObject _CoinPrefab;

	void Start()
	{
		_GroundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
		if (transform.position.z >= 20)
		{
			SpawnObstacle();
		}
		SpawnCoin();
	}

	void OnTriggerExit(Collider other)
	{
		_GroundSpawner.SpawnTile();
		Destroy(gameObject, 2);
	}

	void Update()
	{

	}

	public void SpawnObstacle()
	{
		GameObject obstacleToSpawn = _ObstaclePrefab;
		float random = Random.Range(0f, 1f);

		if (random < _TallObstacleChance)
		{
			obstacleToSpawn = _TallObstaclePrefab;
		}

		int obstacleSpawnIndex = Random.Range(0, 3);
		Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

		if (!GetComponent<Collider>().bounds.Contains(spawnPoint.position))
		{
			return;
		}

		Instantiate(obstacleToSpawn, spawnPoint.position, Quaternion.identity, transform);
	}

	public void SpawnCoin()
	{
		int coinsToSpawn = Random.Range(0, 5);
		for (int i = 0; i < coinsToSpawn; i++)
		{
			var tempCoin = Instantiate(_CoinPrefab, transform);
			tempCoin.transform.position = GetRandomPointCollider(GetComponent<Collider>());
		}
	}

	Vector3 GetRandomPointCollider(Collider collider)
	{
		Vector3 point = new Vector3(
			Random.Range(collider.bounds.min.x, collider.bounds.max.x),
			Random.Range(collider.bounds.min.y, collider.bounds.max.y),
			Random.Range(collider.bounds.min.z, collider.bounds.max.z)
			);


		if (!collider.bounds.Contains(point))
		{
			point = GetRandomPointCollider(collider);
		}

		point.y = 1;
		return point;
	}
}
