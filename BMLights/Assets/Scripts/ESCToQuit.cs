using UnityEngine;

public class ESCToQuit : MonoBehaviour 
{
	public KeyCode keyToQuit = KeyCode.Escape;
	
	void Update () 
	{
		if(keyToQuit != KeyCode.None && Input.GetKeyDown(keyToQuit))
			Application.Quit();
	}
}
