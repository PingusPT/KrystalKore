using System.Collections;

using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    static public MenuScript intance;

    AudioSource src;
    [SerializeField] AudioClip MainMusic, MainMusicLoop;

    [SerializeField] Image Logo, Play, Quit, Load;

    VideoPlayer video;

    bool flag = true;
    bool flag2 = true;
    public float fadeSpeed =0.5f;
    float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        if (!intance)
        {
            intance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        StartObject();
    }

    private void Awake()
    {
        StartObject();
    }

    public void StartObject()
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

        if(SceneManagerScript.instance.NeedNewGame() && flag2)
        {
            src.Stop();
            video.Play();
            flag2 = false;
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
        
        Debug.Log("ALABASTA " + targetImage);
        Color originalColor = targetImage.color;
        

        while (elapsedTime < fadeSpeed)
        {
            Debug.Log(elapsedTime +  " ! " + fadeSpeed);
            targetImage.color = Color.Lerp(originalColor, Color.clear, elapsedTime / fadeSpeed);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        
        targetImage.gameObject.SetActive(false);
    }

}
