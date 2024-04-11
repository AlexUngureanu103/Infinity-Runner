using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TJunction : MonoBehaviour
{
    private GroundSpawner _GroundSpawner;
    private bool playerHasEntered = false; // Add this line


    // Start is called before the first frame update
    void Start()
    {
        _GroundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerHasEntered) // Check the flag here
        {
            playerHasEntered = true; // Set the flag to true
            Destroy(gameObject, 3);
            _GroundSpawner.ChangeTileDirection();
            _GroundSpawner.SpawnTile();
        }
    }

    void OnTriggerExit(Collider other)
    {

    }
}
