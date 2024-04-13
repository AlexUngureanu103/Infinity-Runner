using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItem : MonoBehaviour
{
	[SerializeField]
	private int _BaseCost = 100;
	[SerializeField]
	private string _UpgradeName;
	[SerializeField]
	private string _UpgradeDisplayName;

	private double actualCost;

	private int level;
	[SerializeField]
	private int maxLevel = 8;

	private float coins;

	[SerializeField]
	private TextMeshPro _Name;

	[SerializeField]
	private Button _UpgradeButton;


	void Start()
	{
#if UNITY_EDITOR
		coins = 10000;
		PlayerPrefs.SetFloat("Coins", 10000);
		coins = PlayerPrefs.GetFloat("Coins", 10000);
#else
	coins = PlayerPrefs.GetFloat("Coins", 0);
#endif


		if (string.IsNullOrEmpty(_UpgradeName))
			_UpgradeName = gameObject.name;

		level = PlayerPrefs.GetInt(_UpgradeName, 0);
		actualCost = _BaseCost * Mathf.Pow(2, level);

		_Name.text = _UpgradeDisplayName + '\n' + "Cost: " + actualCost.ToString();
		_UpgradeButton.onClick.AddListener(Upgrade);

		UpdateUpgrade();
	}

	private void UpdateUpgrade()
	{
		for (int i = 0; i < maxLevel; i++)
		{
			var image = transform.GetChild(i + 1).GetComponent<Image>();
			if (i < level)
			{
				image.color = Color.green;
			}
			else
			{
				image.color = Color.gray;
			}
		}
	}

	public void Upgrade()
	{
		//Add 10000 coins for testing
#if UNITY_EDITOR
		Debug.Log("Adding 10000 coins");
		coins += 10000;
		PlayerPrefs.SetFloat("Coins", 0);
#endif

		if (coins >= _BaseCost)
		{
			GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<SoundManager>().PlayUpgradeSound();

			coins -= _BaseCost;
			PlayerPrefs.SetFloat("Coins", coins);
			level++;
			PlayerPrefs.SetInt(_UpgradeName, level + 1);
			actualCost *= 2;
			_Name.text = _UpgradeDisplayName + '\n' + "Cost: " + actualCost.ToString();

			UpdateUpgrade();
		}
		else
		{
			Debug.Log("Not enough coins");
		}
	}

	void Update()
	{
		if (level >= maxLevel)
		{
			_UpgradeButton.interactable = false;
			_Name.text = _UpgradeDisplayName;
		}
		else
			_UpgradeButton.interactable = (coins >= actualCost);
	}
}
