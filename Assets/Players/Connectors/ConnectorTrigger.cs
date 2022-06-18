using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorTrigger : MonoBehaviour
{
	private AudioSource audioSource;
	private AudioClip snap;
	
	public GameObject ControlPopup;
	private float controlPopupYPos;
	
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		snap = Resources.Load<AudioClip>("Audio/snap");
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (PlayerMovement.onTop == "None")
		{
			if (gameObject.name == "BlueUpperConnector(Clone)" &&
			col.gameObject.name == "PinkLowerConnector(Clone)")
			{
				PlayerMovement.onTop = "Pink";
				controlPopupYPos = 1.6f;
				StartCoroutine(SpawnControlPopup());
			}
			if (gameObject.name == "BlueLowerConnector(Clone)" &&
			col.gameObject.name == "PinkUpperConnector(Clone)")
			{
				PlayerMovement.onTop = "Blue";
				controlPopupYPos = 2.7f;
				StartCoroutine(SpawnControlPopup());
			}
			audioSource.PlayOneShot(snap);
		}
	}
	
	IEnumerator SpawnControlPopup()
	{
		yield return new WaitForSeconds(0.05f);
		Instantiate(ControlPopup, new Vector3(transform.position.x,
		transform.position.y + controlPopupYPos, transform.position.z), Quaternion.identity);
	}
}