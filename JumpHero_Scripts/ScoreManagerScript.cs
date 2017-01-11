using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerScript : MonoBehaviour {


	public static ScoreManagerScript instance;

	private Text scoreText;
	private int score;


	void Awake () 
	{
		MakeInstance ();
		scoreText = GameObject.Find ("Score").GetComponent <Text> ();
	}
	

	void MakeInstance () 
	{
		if (instance == null) 
		{
			instance = this;
		}	
	}

	public void IncrementScore()
	{
		score++;
		scoreText.text = score.ToString ();
	}

	public int GetScore()
	{
		return this.score;
	}
}
