using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public bool loop;
        [Range(0f, 1f)]
        public float volume = 1f;
        [HideInInspector]
        public AudioSource source;
    }

    public Sound[] sounds;
    public Sound backgroundMusic;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
        }

        if (backgroundMusic.clip != null)
        {
            backgroundMusic.source = gameObject.AddComponent<AudioSource>();
            backgroundMusic.source.clip = backgroundMusic.clip;
            backgroundMusic.source.loop = true;
            backgroundMusic.source.volume = backgroundMusic.volume;
        }
    }

    void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic.source != null)
        {
            backgroundMusic.source.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (backgroundMusic.source != null)
        {
            backgroundMusic.source.Stop();
        }
    }

    public AudioClip GetAudioClip(string name)
{
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if (s == null)
    {
        Debug.LogWarning("Sound: " + name + " not found!");
        return null;
    }
    return s.clip;
}
}