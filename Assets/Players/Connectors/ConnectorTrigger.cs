using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorTrigger : MonoBehaviour
{
	private AudioSource audioSource;
	private AudioClip snap;
	
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		snap = Resources.Load<AudioClip>("Audio/snap");
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (PlayerMovement.onTop == "None")
		{
			if (gameObject.name == "BlueUpperConnector(Clone)" ||
			col.gameObject.name == "PinkLowerConnector(Clone)")
			{
				PlayerMovement.onTop = "Pink";
			}
			if (gameObject.name == "BlueLowerConnector(Clone)" ||
			col.gameObject.name == "PinkUpperConnector(Clone)")
			{
				PlayerMovement.onTop = "Blue";
			}
			Debug.Log("On top: " + PlayerMovement.onTop);
			audioSource.PlayOneShot(snap);
		}
	}
}