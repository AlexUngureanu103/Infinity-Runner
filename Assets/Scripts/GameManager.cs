using TMPro;
using UnityEngine;

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
        PlayerPrefs.SetInt("Coins", coins);

        _ScoreCoints.text = "Score: " + coins.ToString();
    }

    void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        _ScoreCoints.text = "Score: " + coins.ToString();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
           UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
