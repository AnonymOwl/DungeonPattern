using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void LineEndHandler();

public class LineManager : MonoBehaviour {

	private TrailRenderer tr;
	[HideInInspector]
	public bool isDrawing = false;

	private List<int> circleList;

	public static event LineEndHandler EventLineEnd;

	void Start() {
		tr = GetComponent<TrailRenderer>();		
		circleList = new List<int>();	
	}
	
	void Update() {
		if (Input.touchCount > 0)
		{
			Touch currentTouch = Input.GetTouch(0);
			// Move the "finger" object
			if ((currentTouch.phase == TouchPhase.Began || currentTouch.phase == TouchPhase.Moved))
			{
				var newPosition = Camera.main.ScreenToWorldPoint(new Vector3(currentTouch.position.x, currentTouch.position.y, 0));
				transform.position = new Vector3(newPosition.x, newPosition.y, 0);
			}

			//Starts/Ends the line drawing
			if (currentTouch.phase == TouchPhase.Began)
			{
				isDrawing = true;
				circleList.Clear();
			}
			if (currentTouch.phase == TouchPhase.Ended || currentTouch.phase == TouchPhase.Canceled)
			{
				isDrawing = false;
				//TODO: add an actual circleList action.
				circleList.Clear();
				tr.time = 0.2f;
				OnLineEnd();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Circle" && isDrawing)
		{
			if (circleList.Count == 0)
				tr.time = 5f;
			circleList.Add(other.GetComponent<Circle>().circleNumber);
		}
	}

	public static void OnLineEnd()
	{
		if (EventLineEnd != null)
		{
			EventLineEnd();
		}
	}
}
