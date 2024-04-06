using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private GroundSpawner _GroundSpawner;

    void Start()
    {
        _GroundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    void OnTriggerExit(Collider other)
    {
        _GroundSpawner.SpawnTile();
        Destroy(gameObject, 2);
    }

    void Update()
    {
        
    }
}
