using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMusicScript : MonoBehaviour
{

    [SerializeField] AudioClip GeneralMusic;

    public static WorldMusicScript intance;

    AudioSource src;


    // Start is called before the first frame update
    void Start()
    {
        intance = this;
        src = gameObject.GetComponent<AudioSource>();
        src.volume = 0;
        src.clip = GeneralMusic;
        StartCoroutine(Fade(true, src, 2f, 1f));
        StartCoroutine(Fade(false, src, 2f, 0f));
    }

    void Update()
    {
        if(!src.isPlaying)
        {
            src.Play();
            StartCoroutine(Fade(true, src, 2f, 1f));
            StartCoroutine(Fade(false, src, 2f, 0f));

        }
    }

    public IEnumerator Fade(bool fadeIn, AudioSource source, float duration, float targetVolume)
    {
        if(!fadeIn)
        {
            double SourceLength = (double)source.clip.samples / source.clip.frequency;
            yield return new WaitForSecondsRealtime((float)(SourceLength - duration));
        }

        float time = 0f;
        float StartVolume = source.volume;

        while(time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(StartVolume, targetVolume, time / duration);
            yield return null;
        }

        yield break;
    }

    public AudioClip GetCurrentAudio()
    {
        Debug.Log(src.clip);
        return src.clip;
    }

    public void ChangeTrack(AudioClip audio)
    {
        StopAllCoroutines();

        src.clip = audio;

        src.volume = 0f;
        StartCoroutine(Fade(true, src, 2f, 1f));
        StartCoroutine(Fade(false, src, 2f, 0f));
    }
    
}
