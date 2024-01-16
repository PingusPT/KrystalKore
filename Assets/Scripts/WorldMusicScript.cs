using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMusicScript : MonoBehaviour
{

     [SerializeField] public AudioClip GeneralMusic, TutorialMusic, treadmillMusic, BombMusic, WaterMusic;

    public static WorldMusicScript intance;

    AudioSource src;


    
    void Start()
    {
        intance = this;
        src = gameObject.GetComponent<AudioSource>();
        src.clip = TutorialMusic;
        src.volume = 1f;
        src.Play();

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
        float volumeInicial = 1f;// 1
        Debug.Log("Volume Inicio -  " + src.volume + "Volume Inicio -  " + volumeInicial);
        while (durationTransition > 0)
        {
            src.volume = Mathf.Lerp(volumeInicial, 0, 1 - (durationTransition / duracaoTotal));
            durationTransition -= Time.deltaTime;
            Debug.Log("Volume Meio -  " + src.volume);
            yield return null;
        }
        
        src.Stop();
        Debug.Log( "Volume Final -  " + src.volume);
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
