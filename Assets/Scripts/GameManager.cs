using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int coins;

    public static GameManager _Instance;

    [SerializeField]
    private TextMeshPro _ScoreCoints;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int value = 1)
    {
        coins += value;
        _ScoreCoints.text = "Score: " + coins.ToString();
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
