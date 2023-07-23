using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDialogue : MonoBehaviour
{
    public Text dialogueText;
    private bool isSpeaking = false;
    public Canvas dialogueCanvas;
    private bool canClick;
    private bool clickToExit;
    public float characterDelay = 0.3f;
    public AudioSource dialogueSound;
    private string dialogue;

    private void Start()
    {
        dialogueCanvas.gameObject.SetActive(false);
        canClick = true;
        clickToExit = false;
    }

    private void OnMouseDown()
    {
        if (canClick && clickToExit == false)
        {
            canClick = false;
            clickToExit = true;
            //string dialogue;

            dialogueCanvas.gameObject.SetActive(true);
            dialogueText.text = dialogue;
            StartCoroutine(TypeText());
            isSpeaking = true;
            clickToExit = true;
            StartCoroutine("Wait");
            if (!dialogueSound.isPlaying)
            {
                dialogueSound.Play();
            }
        }

    }

    private void Update()
    {

        if (canClick && isSpeaking && clickToExit && Input.GetButtonUp("Fire1"))
        {
            dialogueCanvas.gameObject.SetActive(false);
            isSpeaking = false;
            clickToExit = false;
        }

    }

    IEnumerator TypeText()
    {
        dialogueText.text = "";
        foreach (char c in dialogue)
        {
            yield return new WaitForSeconds(characterDelay);
            dialogueText.text += c;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        canClick = true;
        //clickToExit = false;
    }
}

