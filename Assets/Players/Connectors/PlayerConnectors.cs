using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConnectors : MonoBehaviour
{
	public GameObject BlueUpperConnector;
	public GameObject BlueLowerConnector;
	public GameObject PinkUpperConnector;
	public GameObject PinkLowerConnector;
	
	private float BlueUpperYPos = 0.7f;
	private float BlueLowerYPos = -0.5f;
	private float PinkUpperYPos = 0.75f;
	private float PinkLowerYPos = -0.1f;
	
	private void UpdatePosition(GameObject connector, float f)
	{
		connector.transform.position =
		new Vector2(transform.position.x, transform.position.y + f);
	}
	
    void Start()
    {
		if (gameObject.name == "Blue")
		{
			BlueUpperConnector = Instantiate(BlueUpperConnector,
			new Vector2(transform.position.x, transform.position.y + BlueUpperYPos),
			Quaternion.identity);
			BlueLowerConnector = Instantiate(BlueLowerConnector,
			new Vector2(transform.position.x, transform.position.y + BlueLowerYPos),
			Quaternion.identity);
		}
		if (gameObject.name == "Pink")
		{
			PinkUpperConnector = Instantiate(PinkUpperConnector,
			new Vector2(transform.position.x, transform.position.y + PinkUpperYPos),
			Quaternion.identity);
			PinkLowerConnector = Instantiate(PinkLowerConnector,
			new Vector2(transform.position.x, transform.position.y + PinkLowerYPos),
			Quaternion.identity);
		}
    }
	
    void Update()
    {
        if (gameObject.name == "Blue")
		{
			UpdatePosition(BlueUpperConnector, BlueUpperYPos);
			UpdatePosition(BlueLowerConnector, BlueLowerYPos);
		}
		if (gameObject.name == "Pink")
		{
			UpdatePosition(PinkUpperConnector, PinkUpperYPos);
			UpdatePosition(PinkLowerConnector, PinkLowerYPos);
		}
    }
}
