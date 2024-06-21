using UnityEngine;

public class AudioVibroManager : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _buySound;
    private AudioSource _audioSource;
    public static bool isVibro;

    [SerializeField] private GameObject _soundOn;
    [SerializeField] private GameObject _soundOff;
    [SerializeField] private GameObject _vibroOn;
    [SerializeField] private GameObject _vibroOff;
    

    private void Start()
    {
        Vibration.Init();
        _audioSource = GetComponent<AudioSource>();
        int vibro = PlayerPrefs.GetInt("vibro", 1);
        if (vibro == 1)
        {
            _vibroOff.SetActive(false);
            _vibroOn.SetActive(true);
            isVibro = true;
        }
        else
        {
            _vibroOn.SetActive(false);
            _vibroOff.SetActive(true);
            isVibro = false;
        }

        int sound = PlayerPrefs.GetInt("Sound", 1);
        if (sound == 1) SoundOn();
        else SoundOff();
    }

    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
        if (isVibro) Vibration.VibrateIOS(ImpactFeedbackStyle.Light);
    }

    public void PlayBuySound()
    {
        _audioSource.PlayOneShot(_buySound);
        if (isVibro) Vibration.VibrateIOS(NotificationFeedbackStyle.Success);
    }

    public void SoundOff()
    {
        _soundOn.SetActive(false);
        _soundOff.SetActive(true);
        AudioListener.volume = 0;
        PlayerPrefs.SetInt("Sound", 0);
        if (isVibro) Vibration.VibrateIOS(ImpactFeedbackStyle.Light);
    }

    public void SoundOn()
    {
        _soundOff.SetActive(false);
        _soundOn.SetActive(true);
        AudioListener.volume = 1;
        PlayerPrefs.SetInt("Sound", 1);
        if (isVibro) Vibration.VibrateIOS(ImpactFeedbackStyle.Light);
    }

    public void VibroOff()
    {
        _vibroOn.SetActive(false);
        _vibroOff.SetActive(true);
        isVibro = false;
        PlayerPrefs.SetInt("vibro", 0);
        if (isVibro) Vibration.VibrateIOS(ImpactFeedbackStyle.Light);
    }

    public void VibroOn()
    {
        _vibroOff.SetActive(false);
        _vibroOn.SetActive(true);
        isVibro = true;
        PlayerPrefs.SetInt("vibro", 1);
        if (isVibro) Vibration.VibrateIOS(ImpactFeedbackStyle.Light);
    }
}