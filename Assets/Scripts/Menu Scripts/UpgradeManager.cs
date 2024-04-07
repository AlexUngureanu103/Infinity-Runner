using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private int coins;

    public static UpgradeManager _Instance;

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
    void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        _ScoreCoints.text = "Coins: " + coins.ToString();
    }

    void Update()
    {
        
    }
}
