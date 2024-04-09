using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _GroundTile;
    [SerializeField]
    private GameObject _GroundTileLeft;
    [SerializeField]
    private GameObject _GroundTileRight;
    [SerializeField]
    private Rigidbody Player;

    enum SpawnPosition
    {
        None,
        Left,
        Middle,
        Right
    }

    private SpawnPosition lastSpawnPosition;


    private Vector3 _NextSpawnPoint = new Vector3(0, 0, 0);

    public void SpawnTile()
    {
        if(_NextSpawnPoint.z - Player.transform.position.z > 200)
        {
            return;
        }   
        // Generate a random number between 0 and 2
        int randomNumber = Random.Range(0, 3);

        GameObject tileToSpawn;

        switch (randomNumber)
        {
            case 0:
                tileToSpawn = _GroundTile;
                break;
            case 1:
                if (lastSpawnPosition == SpawnPosition.Right)
                {
                    lastSpawnPosition = SpawnPosition.Middle;
                    tileToSpawn = _GroundTile;
                }
                else
                {
                    lastSpawnPosition = SpawnPosition.Left;
                    tileToSpawn = _GroundTileLeft;
                }
                break;
            case 2:
                if (lastSpawnPosition == SpawnPosition.Left)
                {
                    lastSpawnPosition = SpawnPosition.Middle;
                    tileToSpawn = _GroundTile;
                }
                else
                {
                    lastSpawnPosition = SpawnPosition.Right;
                    tileToSpawn = _GroundTileRight;
                }
                break;
            default:
                tileToSpawn = _GroundTile;
                break;
        }

        var temp = Instantiate(tileToSpawn, _NextSpawnPoint, Quaternion.identity);
        _NextSpawnPoint = temp.transform.GetChild(3).transform.position;
    }

    void Start()
    {
        lastSpawnPosition = SpawnPosition.None;
        for (int i = 0; i < 15; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {

    }
}
