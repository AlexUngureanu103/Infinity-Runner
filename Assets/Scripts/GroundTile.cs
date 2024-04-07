using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private GroundSpawner _GroundSpawner;

    [SerializeField]
    private GameObject _ObstaclePrefab;

    void Start()
    {
        _GroundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        SpawnObstacle();
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
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        Instantiate(_ObstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
    }
}
