using UnityEngine;

public class GameAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _bonusSound;
    [SerializeField] private AudioClip _coinSound;
    private AudioSource _audioSource;
    public static bool isVibro;

    private void Start()
    {
        Vibration.Init();
        int vibro = PlayerPrefs.GetInt("vibro", 1);
        if (vibro == 1) isVibro = true;
        else isVibro = false;
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
        if (isVibro) Vibration.VibrateIOS(ImpactFeedbackStyle.Light);
    }

    public void PlayBonusSound()
    {
        _audioSource.PlayOneShot(_bonusSound);
        if (isVibro) Vibration.VibrateIOS(ImpactFeedbackStyle.Rigid);
    }

    public void PlayCoinSound()
    {
        _audioSource.PlayOneShot(_coinSound);
        if (isVibro) Vibration.VibrateIOS(ImpactFeedbackStyle.Heavy);
    }
}