using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

	public int circleNumber;

	private bool isActive = false;

	void Awake()
	{
		//LineManager lm = GameObject.Find("Line").GetComponent<LineManager>();
		LineManager.EventLineEnd += new LineEndHandler(Deactivate);
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && other.GetComponent<LineManager>().isDrawing)
			Activate();
	}

	void Activate()
	{
		transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.3f, 1f, 0.3f); 
		isActive = true;
	}

	void Deactivate()
	{
		transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f); 
		isActive = false;
	}
}
