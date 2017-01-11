using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CircleRotateScript : MonoBehaviour {


	[SerializeField]
	private float rotationSpeed = 100f;

	private bool canRotate;
	private float angle;

	// Use this for initialization
	void Awake () 
	{
		canRotate = true;
		StartCoroutine (ChangeRotationSpeed ());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canRotate) 
		{
			RotateTheCircle ();
		}	
	}

	IEnumerator ChangeRotationSpeed()
	{
		yield return new WaitForSeconds(1f);

		if (Random.Range (0, 2) > 0) {
			rotationSpeed = -Random.Range (150, 300);
		} else {
			rotationSpeed = Random.Range (150, 300);
		}

		StartCoroutine (ChangeRotationSpeed ());
	}



	void RotateTheCircle()
	{
		angle = transform.rotation.eulerAngles.z;
		angle += rotationSpeed * Time.deltaTime;
		transform.rotation = Quaternion.Euler (new Vector3(0,0,angle));
	}
}
