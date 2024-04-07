using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int _coinValue = 1;

    [SerializeField]
    private float _rotationSpeed = 90.0f;
    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager._Instance.AddCoins(_coinValue);
        }
        else if(other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
