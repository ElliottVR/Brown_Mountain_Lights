using UnityEngine;
using System.Collections;

public class CloudsMove : MonoBehaviour {

	private GameObject player;

	public Vector3 offset;



	void Start ()
	{
		StartCoroutine("FindPlayer");
	}

	void LateUpdate ()
	{
		transform.position = player.transform.position + offset;
	}

	IEnumerator FindPlayer()
	{
		yield return new WaitForSeconds(1f);
		player = GameObject.FindGameObjectWithTag("Player");
		yield return new WaitForSeconds(0.5f);
		offset = transform.position - player.transform.position;

	}
}