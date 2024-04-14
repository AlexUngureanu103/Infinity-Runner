using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField]
    private AudioSource _AudioSource;
    [SerializeField]
    private AudioSource _SFXSource;
    [SerializeField]
    private AudioClip _CoinCollectSound;
    [SerializeField]
    private AudioClip _DeathSound;
    [SerializeField]
    private AudioClip _JumpSound;
    [SerializeField]
    private AudioClip _MenuSound;
    [SerializeField]
    private AudioClip _BackgroundMusic;
    [SerializeField]
    private AudioClip _UpgradeSound;

    public void SetMusicVolume(float volume)
    {
        _AudioSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        _SFXSource.volume = volume;
    }

    public void PlayCoinCollectSound()
    {
        _SFXSource.clip = _CoinCollectSound;
        _SFXSource.Play();
    }

    private void Start()
    {
        var musicVolumeSliderValue = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        var sFXVolumeSliderValue = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        Instance.SetMusicVolume(musicVolumeSliderValue);
        Instance.SetSFXVolume(sFXVolumeSliderValue);

        _AudioSource.clip = _BackgroundMusic;
        _AudioSource.loop = true;
        _AudioSource.Play();
    }

    public void PlayDeathSound()
    {
        _SFXSource.clip = _DeathSound;
        _SFXSource.Play();
    }

    public void PlayJumpSound()
    {
        _SFXSource.clip = _JumpSound;
        _SFXSource.Play();
    }

    public void PlayMenuSound()
    {
        _SFXSource.clip = _MenuSound;
        _SFXSource.Play();
    }

    public void PlayUpgradeSound()
    {
        _SFXSource.clip = _UpgradeSound;
        _SFXSource.Play();
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Make it persist across scene loads
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // Destroy any duplicates
        }
    }
}