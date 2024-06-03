using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVibroManager : MonoBehaviour
{
    private static AudioVibroManager instance;
    [SerializeField] private AudioClip _clickSound;
    private AudioSource _audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }
}
