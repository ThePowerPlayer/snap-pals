using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVariables : MonoBehaviour
{
	Camera cam;
	private GameObject Blue;
	public static float blueScreenPos;
	
    void Start()
    {
        cam = GetComponent<Camera>();
		Blue = GameObject.FindWithTag("Blue");
    }
	
    void Update()
    {
        blueScreenPos = cam.WorldToScreenPoint(Blue.transform.position).x;
		
		// Game over!
		if (GlobalVariables.lives <= 0)
		{
			Destroy(this);
		}
    }
}
