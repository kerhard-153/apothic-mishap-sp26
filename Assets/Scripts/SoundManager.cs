using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource sfxSource;
    public AudioSource musicSource;

    public Sound[] sounds;

    private Dictionary<string, Sound> soundDictionary;

    private void Awake()
    {
        // setup
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // dictionary
        soundDictionary = new Dictionary<string, Sound>();

        foreach (Sound sound in sounds)
        {
            soundDictionary[sound.name] = sound;
        }
    }

    public void Play(string soundName)
    {
        if (!soundDictionary.ContainsKey(soundName))
        {
            Debug.LogWarning("Sound not found: " + soundName);
            return;
        }

        Sound sound = soundDictionary[soundName];

        sfxSource.PlayOneShot(sound.clip, sound.volume);
    }

    public void PlayMusic(AudioClip newMusic)
    {
        if (musicSource.clip == newMusic)
            return;

        musicSource.clip = newMusic;
        musicSource.Play();
    }
}
