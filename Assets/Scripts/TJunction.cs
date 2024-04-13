using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TJunction : MonoBehaviour
{
    private GroundSpawner _GroundSpawner;
    private GameObject player;
    private bool playerHasEntered = false; // Add this line
    private bool actionCompleted = false;


    // Start is called before the first frame update
    void Start()
    {
        _GroundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHasEntered && !actionCompleted)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 25)
            {
                return;
            }
            actionCompleted = true;
            Destroy(gameObject, 3);
            _GroundSpawner.ChangeTileDirection();
            _GroundSpawner.SpawnTile();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerHasEntered) // Check the flag here
        {
            playerHasEntered = true; // Set the flag to true
            player = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {

    }
}
