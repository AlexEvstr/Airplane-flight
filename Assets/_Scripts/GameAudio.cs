using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSound;
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
        if (isVibro) Vibration.VibratePop();
    }
}
