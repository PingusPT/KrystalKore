using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    AudioSource src;
    [SerializeField] AudioClip MainMusic, MainMusicLoop;

    [SerializeField] Image Logo, Play, Quit, Load;

    VideoPlayer video;

    bool flag = true;
    public float fadeSpeed =0.5f;

    // Start is called before the first frame update
    void Start()
    {
        src = gameObject.GetComponent<AudioSource>();
        video = gameObject.GetComponent<VideoPlayer>();
        src.clip = MainMusic;
        src.loop = false;
        src.Play();
        video.Pause();

        video.loopPointReached += OnVideoEnd;

    }

    // Update is called once per frame
    void Update()
    {
        if(!src.isPlaying && flag)
        {
            flag = false;
            src.clip = MainMusicLoop;
            src.loop = true;
            src.Play();
        }

        if(SceneManagerScript.instance.NeedNewGame())
        {
            src.Stop();
            video.Play();

            StartCoroutine(FadeOut(Logo));
            StartCoroutine(FadeOut(Play));
            StartCoroutine(FadeOut(Quit));
            StartCoroutine(FadeOut(Load));
        }

        if(video.isPlaying && (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            video.time = video.length - 0.4;
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {

        SceneManagerScript.instance.LoadNewScene();
    }
    private IEnumerator FadeOut(Image targetImage)
    {
        Color originalColor = targetImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeSpeed)
        {
            
            targetImage.color = Color.Lerp(originalColor, new Color(originalColor.r, originalColor.g, originalColor.b, 0f), elapsedTime / fadeSpeed);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        
        targetImage.gameObject.SetActive(false);
    }

}
