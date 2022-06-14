using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
	private int levelToLoadID;
	
	void Start()
	{
		
	}
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
		{
			StartCoroutine(LoadLevel());
		}
    }
	
	IEnumerator LoadLevel()
	{
		// Start loading asynchronous scene
		AsyncOperation asyncLoad = 
		SceneManager.LoadSceneAsync(levelToLoadID);
		
		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
    }
}
