using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    AudioSource audioSource;

    float volume = .3f;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat(PlayerPrefsString.MUSIC_VOLUME, .3f);

        audioSource.volume = volume;
    }

    public void ChangeVolume()
    {
        volume += .1f;

        if (volume > 1f)
        {
            volume = 0f;
        }

        audioSource.volume = volume;

        PlayerPrefs.SetFloat(PlayerPrefsString.MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float ReturnVolume()
    {
        return volume;
    }
}
