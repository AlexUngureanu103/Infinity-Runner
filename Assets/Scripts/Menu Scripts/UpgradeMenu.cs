using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
	[SerializeField]
	private Button _BackButton;

	[SerializeField]
	private Button _RESETALLButton;

	void Start()
	{
		_BackButton.onClick.AddListener(BackToMainMenu);

#if UNITY_EDITOR
		_RESETALLButton.onClick.AddListener(ResetUpgrades);
		_RESETALLButton.gameObject.SetActive(true);
#else
    _RESETALLButton.gameObject.SetActive(false);
#endif
	}

	public void ResetUpgrades()
	{
		GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<SoundManager>().PlayMenuSound();
		PlayerPrefs.SetInt("EJ", 0);
		PlayerPrefs.SetInt("JF", 0);
		PlayerPrefs.SetInt("AS", 0);
		PlayerPrefs.SetInt("DS", 0);
		PlayerPrefs.SetInt("HS", 0);
		PlayerPrefs.SetInt("MS", 0);
		PlayerPrefs.SetInt("BS", 0);
		PlayerPrefs.SetInt("BCV", 0);
		PlayerPrefs.SetInt("CM", 0);

		var HealthPoints = 1;
		var ExtraJumps = 0;
		var JumpForce = 5;
		var AccelerationSpeedMultiplyer = 1.5f;
		var DecelerationSpeedMultiplyer = 1.5f;
		var MaxSpeed = 10;
		var BaseCoinValue = 1;
		var CoinMultiplyer = 1;
		var BaseSpeed = 5;
		var HorizontalSpeedMultiplyer = 2;

		PlayerPrefs.SetInt(nameof(HealthPoints), HealthPoints);

		PlayerPrefs.SetInt(nameof(ExtraJumps), ExtraJumps);
		PlayerPrefs.SetFloat(nameof(JumpForce), JumpForce);

		PlayerPrefs.SetFloat(nameof(AccelerationSpeedMultiplyer), AccelerationSpeedMultiplyer);
		PlayerPrefs.SetFloat(nameof(DecelerationSpeedMultiplyer), DecelerationSpeedMultiplyer);
		PlayerPrefs.SetFloat(nameof(HorizontalSpeedMultiplyer), HorizontalSpeedMultiplyer);
		PlayerPrefs.SetFloat(nameof(MaxSpeed), MaxSpeed);
		PlayerPrefs.SetFloat(nameof(BaseSpeed), BaseSpeed);

		PlayerPrefs.SetFloat(nameof(BaseCoinValue), BaseCoinValue);
		PlayerPrefs.SetFloat(nameof(CoinMultiplyer), CoinMultiplyer);

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void BackToMainMenu()
	{
		GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<SoundManager>().PlayMenuSound();
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
