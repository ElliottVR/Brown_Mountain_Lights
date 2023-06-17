using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicIntro : MonoBehaviour
{
    public AudioSource MenuMusic;
    public AudioSource MainMusic;
    public float MaxVolume = 0.5f;
    public float MaxMainVolume = 0.3f;
    //public bool startFade;

    private bool canFade = true;
    private bool cutScene = false;
    //private bool MainScene = false;
    //private bool mainScene = false;

    // Start is called before the first frame update
    void Start()
    {
        MenuMusic.Play();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canFade == true && cutScene == false)
        {
            canFade = false;
            cutScene = true;
            StartCoroutine("StartFade");
            StartCoroutine("MainMusicPlay");
        }

        if (Input.GetKeyDown(KeyCode.Return) && canFade == true && cutScene == true)
        {
            canFade = false;
            //cutScene = true;
            StartCoroutine("StartMainFade");
        }
    }

    IEnumerator StartFade()
    {
        float t = MaxVolume;
        while (t > 0)
        {
            t -= Time.deltaTime / 20;
            MenuMusic.volume = t;
            yield return new WaitForSeconds(0);
        }

        MenuMusic.volume = 0.0f;
        MenuMusic.Stop();
        MenuMusic.loop = false;
        canFade = true;

        //Destroy(gameObject);
    }

    IEnumerator StartMainFade()
    {
        yield return new WaitForSeconds(50);
        MainMusic.loop = false;
        float t = MaxMainVolume;
        while (t > 0)
        {
            t -= Time.deltaTime / 50;
            MainMusic.volume = t;
            yield return new WaitForSeconds(0);
        }

        MainMusic.volume = 0.0f;
        MainMusic.Stop();
        //MainMusic.loop = false;
        canFade = true;

        Destroy(gameObject);
    }

    IEnumerator MainMusicPlay()
    {

        yield return new WaitForSeconds(5);
        MainMusic.Play();

    }
}
