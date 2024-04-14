using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	[SerializeField]
	private Button _BackButton;
	[SerializeField]
	private Slider _MusicVolumeSlider;
	[SerializeField]
	private Slider _SFXVolumeSlider;

	void Start()
	{
		_BackButton.onClick.AddListener(BackToMainMenu);
		_MusicVolumeSlider.onValueChanged.AddListener(HandleMusicVolumeChanged);
		_SFXVolumeSlider.onValueChanged.AddListener(HandleSFXVolumeChanged);

		_MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
		_SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
	}

	private void HandleMusicVolumeChanged(float volume)
	{
		SoundManager.Instance.SetMusicVolume(volume);
		PlayerPrefs.SetFloat("MusicVolume", volume);
	}

	private void HandleSFXVolumeChanged(float volume)
	{
		SoundManager.Instance.SetSFXVolume(volume);
		PlayerPrefs.SetFloat("SFXVolume", volume);
	}

	public void BackToMainMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}