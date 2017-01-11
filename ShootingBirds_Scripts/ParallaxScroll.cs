using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour {

	public float parallaxFactor;

	private Vector3 prevCamermaPosition;

	// Use this for initialization
	void Start () 
	{
		prevCamermaPosition = Camera.main.transform.position;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 delta = Camera.main.transform.position - prevCamermaPosition;
		delta.y = 0f;
		delta.z = 0f;

		transform.position += delta * parallaxFactor;

		prevCamermaPosition = Camera.main.transform.position;
	}
}
