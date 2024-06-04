using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVibroManager : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _buySound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }

    public void PlayBuySound()
    {
        _audioSource.PlayOneShot(_buySound);
    }
}
