using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoText : MonoBehaviour
{
    void Update()
    {
        if (GlobalVariables.gameStart == true)
		{
			Destroy(gameObject);
		}
    }
}
