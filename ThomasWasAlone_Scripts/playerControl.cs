using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class playerControl : MonoBehaviour {


	private bool inAir;
	private bool active;
	private float jump;

	// Use this for initialization
	void Start () 
	{
		inAir = true;
		active = false;

		if(gameObject.tag == "thomas")
			jump = 6f;
		if(gameObject.tag == "chris")
			jump = 2.5f;

	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetKey ("right") && active==true) 
		{
			gameObject.transform.position = new Vector2 (gameObject.transform.position.x+0.1f,gameObject.transform.position.y);
		}

		if (Input.GetKey ("left")&& active==true) 
		{
			gameObject.transform.position = new Vector2 (gameObject.transform.position.x-0.1f,gameObject.transform.position.y);
		}

		if (Input.GetKeyDown ("space") && inAir==false && active==true) 
		{
			gameObject.transform.position = new Vector2 (gameObject.transform.position.x,gameObject.transform.position.y+jump);
			inAir = true;
		}

	}


	void OnCollisionEnter2D()
	{
		inAir = false;
	}

	void disableControls()
	{
		active = false;
	}

	void enableControls()
	{
		active = true;
	}

	bool status()
	{
		return active;
	}
}
