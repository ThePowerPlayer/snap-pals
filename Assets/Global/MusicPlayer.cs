using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	AudioSource audioSource;
	bool m_Play;
	
    void Start()
    {
		if (GameObject.FindGameObjectsWithTag("MusicPlayer").Length == 1)
		{
			audioSource = GetComponent<AudioSource>();
			DontDestroyOnLoad(gameObject);
			m_Play = true;
			audioSource.Play();
		}
		else if (GameObject.FindGameObjectsWithTag("MusicPlayer").Length > 1)
		{
			Destroy(gameObject);
		}
    }
	
	void Update()
	{
		// Press M to mute/unmute music
		if (Input.GetKeyDown(KeyCode.M))
		{
			// Mute
			if (m_Play == true)
			{
				audioSource.Stop();
				m_Play = false;
			}
			// Unmute
			else if (m_Play == false)
			{
				audioSource.Play();
				m_Play = true;
			}
		}
	}
}