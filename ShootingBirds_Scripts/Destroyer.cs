using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour {



	void OnTriggerEnter2D (Collider2D target) 
	{
		if (target.tag == "Bird" || target.tag == "Pig" || target.tag == "Brick")
		{
			Destroy (target.gameObject);
		}	
	}
}
