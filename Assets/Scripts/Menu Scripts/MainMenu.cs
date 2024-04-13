using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private Button _UpgradeButton;
	[SerializeField]
	private Button _OptionsButton;
	[SerializeField]
	private Button _PlayButton;
	[SerializeField]
	private Button _ExitButton;

	void Start()
	{
		_ExitButton.onClick.AddListener(ExitGame);
		_PlayButton.onClick.AddListener(PlayGame);
		_OptionsButton.onClick.AddListener(ToOptions);
		_UpgradeButton.onClick.AddListener(ToUpgrades);
	}

	public void ExitGame()
	{
		GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<SoundManager>().PlayMenuSound();
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}

	public void PlayGame()
	{
		GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<SoundManager>().PlayMenuSound();

		UnityEngine.SceneManagement.SceneManager.LoadScene(1);
	}

	public void ToOptions()
	{
		GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<SoundManager>().PlayMenuSound();
		UnityEngine.SceneManagement.SceneManager.LoadScene(3);
	}

	public void ToUpgrades()
	{
		GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<SoundManager>().PlayMenuSound();
		UnityEngine.SceneManagement.SceneManager.LoadScene(2);
	}
}
