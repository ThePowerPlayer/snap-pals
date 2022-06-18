using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPopup : MonoBehaviour
{
	private SpriteRenderer sRenderer;
	private Color fade;
	public Sprite HoldMouseButton;
	public Sprite HoldSpace;
	
    void Start()
    {
       sRenderer = GetComponent<SpriteRenderer>(); 
	   fade = sRenderer.color;
	   if (PlayerMovement.onTop == "Blue")
	   {
		   sRenderer.sprite = HoldMouseButton;
	   }
	   else if (PlayerMovement.onTop == "Pink")
	   {
		   sRenderer.sprite = HoldSpace;
	   }
    }

    void FixedUpdate()
    {
        transform.localScale +=
		new Vector3(Time.fixedDeltaTime * 0.5f, Time.fixedDeltaTime * 0.5f, 0f);
		
		fade.a -= Time.fixedDeltaTime * 0.8f;
		sRenderer.color = fade;
		if (sRenderer.color.a <= 0f)
		{
			Destroy(gameObject);
		}
    }
}
