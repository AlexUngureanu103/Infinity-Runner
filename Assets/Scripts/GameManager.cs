using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private float coins;

	public static GameManager _Instance;

	[SerializeField]
	private TextMeshPro _ScoreCoints;

	private float CoinMultiplyer = 1;

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

	public void AddCoins(float value = 1)
	{
		coins += value * CoinMultiplyer;
		GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<SoundManager>().PlayCoinCollectSound();
		PlayerPrefs.SetFloat("Coins", coins);

		_ScoreCoints.text = "Score: " + coins.ToString();
	}

	void Start()
	{
		coins = PlayerPrefs.GetFloat("Coins", 0);
		_ScoreCoints.text = "Score: " + coins.ToString();
		CoinMultiplyer = PlayerPrefs.GetFloat(nameof(CoinMultiplyer), 1);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
	}
}
