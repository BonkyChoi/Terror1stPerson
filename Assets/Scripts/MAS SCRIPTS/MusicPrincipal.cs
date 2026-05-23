using UnityEngine;
using System.Collections;

public class MusicTriggerFade : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource musicSource;
    
    [Header("Volumen")]
    public float normalVolume = 1f;
    public float lowVolume = 0.2f;

    [Header("Velocidad")]
    public float speed = 1f;

    private Coroutine coroutine;

    private void Start()
    {
        musicSource.volume = normalVolume;

        if (!musicSource.isPlaying)
            musicSource.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartFade(lowVolume);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartFade(normalVolume);
        }
    }
    void StartFade(float targetVolume)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(BajarVolume(targetVolume));
    }
    IEnumerator BajarVolume(float target)
    {
        while (Mathf.Abs(musicSource.volume - target) > 0.01f)
        {
            musicSource.volume = Mathf.Lerp(
                musicSource.volume,
                target,
                speed * Time.deltaTime
            );

            yield return null;
        }

        musicSource.volume = target;
    }
}