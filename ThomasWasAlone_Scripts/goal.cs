using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
		

	void OnTriggerEnter2D(Collider2D col)
	{
		if(gameObject.name.StartsWith (col.gameObject.tag))
		{
			GameObject.Find ("_GM").SendMessage ("EngageGoal"+col.gameObject.tag+"");
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if(gameObject.name.StartsWith (col.gameObject.tag))
		{
			GameObject.Find ("_GM").SendMessage ("DisengageGoal"+col.gameObject.tag+"");
		}
	}
}
