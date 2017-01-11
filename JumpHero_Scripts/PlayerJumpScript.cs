﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJumpScript : MonoBehaviour {

	public static PlayerJumpScript instance;

	private Rigidbody2D myBody;
	private Animator anim;

	private float forceX, forceY;
	private float tresholdX = 7f;
	private float tresholdY = 14f;

	private bool setPower, didJump;

	private Slider powerBar;
	private float powerBarTreshold = 10f;
	private float powerBarValue = 0f;


	void Awake () 
	{
		MakeInstance ();
		Initialize ();
	}

	void Initialize()
	{
		powerBar = GameObject.Find ("PowerBar").GetComponent <Slider> ();
		myBody = GetComponent <Rigidbody2D> ();
		anim = GetComponent <Animator> ();


		powerBar.minValue = 0f;
		powerBar.maxValue = 10f;
		powerBar.value = powerBarValue;
	}

	void Update()
	{
		SetPower ();
	}

	void MakeInstance()
	{
		if (instance == null) 
		{
			instance = this;
		}
	}

	void SetPower()
	{
		if (setPower) 
		{
			forceX += tresholdX * Time.deltaTime;
			forceY += tresholdY * Time.deltaTime;

			if (forceX > 6.5f) 
				forceX = 6.5f;

			if (forceY > 13.5f) 
				forceY = 13.5f;

			powerBarValue += powerBarTreshold * Time.deltaTime;
			powerBar.value = powerBarValue;
		}
	}

	public void SetPower(bool setPower)
	{
		this.setPower = setPower;

		if (setPower) {
			Debug.Log ("We are setting the power");
		} else {
			Debug.Log ("We are not setting the power");
		}

		if (!setPower) 
		{
			Jump ();
		}
	}

	void Jump()
	{
		myBody.velocity = new Vector2 (forceX, forceY);
		forceX = forceY = 0f;
		didJump = true;

		anim.SetBool ("jump",didJump);
		gameObject.GetComponent <AudioSource> ().Play ();

		powerBarValue = 0f;
		powerBar.value = powerBarValue;
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (didJump)
		{
			didJump = false;
			anim.SetBool ("Jump",didJump);

			if (target.tag == "Platform") 
			{
				if (GameManager.instance != null) 
				{
					GameManager.instance.CreateNewPlatformAndLerp (target.transform.position.x);
				}

				if (ScoreManagerScript.instance != null) 
				{
					ScoreManagerScript.instance.IncrementScore ();
				}
			}

			if (target.tag == "Dead") 
			{
				if (GameOverManager.instance != null) 
				{
					GameOverManager.instance.GameOverShowPanel ();
					target.GetComponent <AudioSource>().Play ();
				}
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerExit2D(Collider2D target)
	{
		target.tag = "Visited";
	}

}
