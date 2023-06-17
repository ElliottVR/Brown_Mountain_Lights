using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;



public class Travel : MonoBehaviour
{
    private Scene currentScene;

    public string destinationScene;

    private GameObject Player;

    public Vector3 destinationVector;

    private VisualFade visualFadeScript;


    //private FirstPersonController firstPersonController;

    // Start is called before the first frame update
    void Start()
    {
        GameVariables.isTraveling = false;
        currentScene = SceneManager.GetActiveScene();
        //Player = GameObject.FindGameObjectWithTag("Player");
        //Player.GetComponent<FirstPersonController>().enabled = false;
        StartCoroutine("StopMovement");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Traveling. . .");
            GameVariables.isTraveling = true;
            StartCoroutine("WaitToTravel");
        }
    }

    IEnumerator WaitToTravel()
    {
        yield return new WaitForSeconds(1);
        Player.GetComponent<FirstPersonController>().enabled = false;
        Player.transform.position = destinationVector;
        SceneManager.LoadScene(destinationScene);
    }

    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(0.5f);
        Player = GameObject.FindGameObjectWithTag("Player");
        yield return new WaitForSeconds(0.2f);
        Player.GetComponent<FirstPersonController>().enabled = false;
        yield return new WaitForSeconds(1);
        Player.GetComponent<FirstPersonController>().enabled = true;
    }

}
