using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private PlayerMovement _PlayerMovement;

    void Start()
    {
        _PlayerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _PlayerMovement.Die();
        }
    }


    void Update()
    {

    }
}
