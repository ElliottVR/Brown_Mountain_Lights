using UnityEngine;

public class HideMouse : MonoBehaviour 
{
	void Start () 
	{
		Cursor.visible = false;
		Screen.lockCursor = true;
	}
}
