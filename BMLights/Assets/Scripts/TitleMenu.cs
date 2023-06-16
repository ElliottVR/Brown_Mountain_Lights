using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public GameObject FadeCube;
    private VisualFade visualFadeScript;

    public GameObject ContinueText;

    // Start is called before the first frame update
    void Start()
    {
        visualFadeScript = FadeCube.GetComponent<VisualFade>();
        StartCoroutine("StartText");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine("NewScene");
        }
    }

    IEnumerator NewScene()
    {
        ContinueText.SetActive(false);
        visualFadeScript.FadeIn();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Bunker");
    }

    IEnumerator StartText()
    {
        yield return new WaitForSeconds(15);
        ContinueText.SetActive(true);
    }
}
