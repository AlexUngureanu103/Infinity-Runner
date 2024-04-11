using System.Collections;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [SerializeField]
    private Rigidbody Player;
    [SerializeField]
    private float _MaxDistance = 300f; // The maximum distance an object can be before it's destroyed

    void Start()
    {
        StartCoroutine(DestroyObjects());
    }

    IEnumerator DestroyObjects()
    {
        while (true)
        {
            // Get all game objects in the scene
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

            // Loop through all game objects
            foreach (GameObject obj in allObjects)
            {
                // Calculate the distance to the player
                var distance = Vector3.Distance(obj.transform.position, Player.transform.position);
                //float distanceToPlayer = Vector3.Distance(obj.transform.position, transform.position);

                // If the object is more than maxDistance units away from the player and its y-coordinate is between -2 and 2, destroy it

                if (distance > _MaxDistance && obj.transform.position.y >= -2 && obj.transform.position.y <= 2)
                {
                    Destroy(obj);
                }
            }

            yield return new WaitForSeconds(1); // Wait for 1 second before the next check
        }
    }
}
