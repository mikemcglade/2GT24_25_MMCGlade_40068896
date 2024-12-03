using UnityEngine;
using System.Collections;



public class SoundTrigger : MonoBehaviour
{
    public string soundToPlay = "ProximitySound";
    public float triggerDistance = 5f;
    public float fadeTime = 0.5f;

    private Transform playerTransform;
    private AudioSource audioSource;
    private bool isPlaying = false;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = SoundManager.Instance.GetAudioClip(soundToPlay);
        audioSource.loop = true;
        audioSource.volume = 0f;

    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);


        if (distance <= triggerDistance && !isPlaying)
        {

            StartCoroutine(FadeAudioSource.StartFade(audioSource, fadeTime, 1f));
            isPlaying = true;
        }
        else if (distance > triggerDistance && isPlaying)
        {

            StartCoroutine(FadeAudioSource.StartFade(audioSource, fadeTime, 0f));
            isPlaying = false;
        }
    }
}

public static class FadeAudioSource
{
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        audioSource.volume = targetVolume;
    }
}