using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMusicScript : MonoBehaviour
{

    [SerializeField] public AudioClip GeneralMusic, TutorialMusicInicio, TutorialMusic, treadmillMusicInicio, treadmillMusic, BombMusicInicio, BombMusic, WaterMusicInicio, WaterMusic;

    public static WorldMusicScript intance;

    AudioSource src;

    [Range(0f, 2f)]
    public float targetVolume = 1;

    void Start()
    {
        intance = this;
        src = gameObject.GetComponent<AudioSource>();
        src.clip = TutorialMusicInicio;
        src.volume = 1f;
        src.Play();
        src.loop = false;

    }

    private void Update()
    {
        if (!src.isPlaying)
        {
            if (src.clip == TutorialMusicInicio)
            {
                src.clip = TutorialMusic;
                src.loop = true;
                src.Play();
            }
            else if (src.clip == treadmillMusicInicio)
            {
                src.clip = treadmillMusic;
                src.loop = true;
                src.Play();
            }
            else if (src.clip == BombMusicInicio)
            {
                src.clip = BombMusic;
                src.loop = true;
                src.Play();
            }
            else if (src.clip == WaterMusicInicio)
            {
                src.clip = WaterMusic;
                src.loop = true;
                src.Play();
            }
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


    public void ChangeTrack(AudioClip audio, float duration)
    {
        if(src.clip != audio)
        {
            StopAllCoroutines();

            StartCoroutine(BaixarVolumeEAtualizarMusica(audio, duration));
        }
        
    }


    private IEnumerator BaixarVolumeEAtualizarMusica(AudioClip newMusic, float durationTransition)
    {
        float duracaoTotal = durationTransition;
        float volumeInicial = targetVolume;// 1
        
        while (durationTransition > 0)
        {
            src.volume = Mathf.Lerp(volumeInicial, 0, 1 - (durationTransition / duracaoTotal));
            durationTransition -= Time.deltaTime;
            
            yield return null;
        }
        
        src.Stop();
        
        src.clip = newMusic;
        src.Play();

        durationTransition = duracaoTotal;

        while (durationTransition > 0)
        {
            src.volume = Mathf.Lerp(0, volumeInicial, 1 - (durationTransition / duracaoTotal));
            durationTransition -= Time.deltaTime;
            yield return null;
        }

        src.volume = volumeInicial;
    }
}
