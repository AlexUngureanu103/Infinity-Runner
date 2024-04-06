using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _GroundTile;

    private Vector3 _NextSpawnPoint = new Vector3(0, 0, 0);

    public void SpawnTile()
    {
        var temp = Instantiate(_GroundTile, _NextSpawnPoint, Quaternion.identity);
        _NextSpawnPoint = temp.transform.GetChild(1).transform.position;
    }

    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {

    }
}
