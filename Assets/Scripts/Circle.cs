using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class Circle : MonoBehaviour {

	public int circleNumber;

	private bool isActive = false;
	private AndroidJavaObject vibrator;
	private long vibrationDuration = 50;
	private ParticleSystem effectSystem;
	private SpriteRenderer sprite;

	void Awake()
	{
		LineManager.EventLineEnd += new LineEndHandler(Deactivate);

		effectSystem = GetComponentInChildren<ParticleSystem>();
		sprite = GetComponentInChildren<SpriteRenderer>();
		if (Application.platform == RuntimePlatform.Android) {
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && other.GetComponent<LineManager>().isDrawing)
			Activate();
	}

	void Activate()
	{
		if (!isActive) {
			if (Application.platform == RuntimePlatform.Android)
				vibrator.Call("vibrate", vibrationDuration);
			if (effectSystem != null)
				effectSystem.Play();
			sprite.color = new Color(0.3f, 1f, 0.3f); 
			isActive = true;
		}
	}

	void Deactivate()
	{
		if (isActive) {
			sprite.color = new Color(1f, 1f, 1f); 
			isActive = false;
		}
	}
}
