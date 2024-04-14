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
    [SerializeField]
    private GameObject _GreenCoinPrefab;
    [SerializeField]
    private GameObject _BlueCoinPrefab;
    [SerializeField]
    private GameObject _RedCoinPrefab;

    [SerializeField]
    private float _GreenCoinChance = 0.35f;
    [SerializeField]
    private float _BlueCoinChance = 0.15f;
    [SerializeField]
    private float _RedCoinChance = 0.05f;

    private Anim_Movement _Player;

    void Start()
    {
        _Player = GameObject.FindObjectOfType<Anim_Movement>();
        _GroundSpawner = GameObject.FindObjectOfType<GroundSpawner>();

        if (Vector3.Distance(transform.position, _Player.transform.position) > 30f)
        {
            SpawnObstacle();
        }
        SpawnCoin();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Destroy(gameObject, 3);
        }
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

        //if (!GetComponent<Collider>().bounds.Contains(spawnPoint.position))
        //{
        //    return;
        //}

        Instantiate(obstacleToSpawn, spawnPoint.position, transform.rotation, transform);
    }

    public void SpawnCoin()
    {
        int coinsToSpawn = Random.Range(0, 5);
        for (int i = 0; i < coinsToSpawn; i++)
        {
            var randomProbability = Random.Range(0f, 1f);

            GameObject tempCoin;

            if (randomProbability < _RedCoinChance)
            {
                tempCoin = Instantiate(_RedCoinPrefab, transform);
            }
            else if (randomProbability < _BlueCoinChance)
            {
                tempCoin = Instantiate(_BlueCoinPrefab, transform);
            }
            else if (randomProbability < _GreenCoinChance)
            {
                tempCoin = Instantiate(_GreenCoinPrefab, transform);
            }
            else
            {
                tempCoin = Instantiate(_CoinPrefab, transform);
            }

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
