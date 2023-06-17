using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualFade : MonoBehaviour
{
    public Renderer FadeCube;

    public float fadeSpeed = 0.05f;

    private float currentFadeAlpha;

    private float desiredFadeAlpha;

    private bool fadeOut = false;

    private bool fadeIn = false;

    private bool beginFade = true;

    // Start is called before the first frame update
    void Start()
    {
        currentFadeAlpha = 1;
        FadeCube.material.color = new Color(0, 0, 0, 1);
        StartCoroutine("StartFadeOut");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameVariables.isTraveling == true && beginFade == true)
        {
            beginFade = false;
            StartCoroutine("ImmediateBlack");
        }

        if (fadeOut == true)
        {
            currentFadeAlpha = Mathf.MoveTowards(currentFadeAlpha, desiredFadeAlpha, fadeSpeed * Time.deltaTime);
            FadeCube.material.color = new Color(0, 0, 0, currentFadeAlpha);
        }

        if (fadeIn == true)
        {
            currentFadeAlpha = Mathf.MoveTowards(currentFadeAlpha, desiredFadeAlpha, fadeSpeed * Time.deltaTime);
            FadeCube.material.color = new Color(0, 0, 0, currentFadeAlpha);
        }

    }

    public void FadeOut()
    {
        StartCoroutine("StartFadeOut");
    }

    IEnumerator StartFadeOut()
    {
        FadeCube.material.color = new Color(0, 0, 0, 1);
        desiredFadeAlpha = 0;
        yield return new WaitForSeconds(1);
        fadeOut = true;
        beginFade = true;
    }

    public void FadeIn()
    {
        StartCoroutine("StartFadeIn");
    }

    IEnumerator StartFadeIn()
    {
        FadeCube.material.color = new Color(0, 0, 0, 0);
        desiredFadeAlpha = 1;
        yield return new WaitForSeconds(1);
        fadeIn = true;
    }

    IEnumerator ImmediateBlack()
    {
        FadeCube.material.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(2);
        FadeOut();
    }
}
