using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager instance;

	private Text scoreText;
	private int score;

	// Use this for initialization
	void Awake () 
	{
		
		if (instance == null)
			instance = this;
		if (SceneManager.GetActiveScene ().name != "MainMenu") {
			scoreText = GameObject.Find ("Score Text").GetComponent <Text> ();
		}
	}

	public void setScore()
	{
		if (this != null && SceneManager.GetActiveScene ().name != "MainMenu")
		{
			score++;
			scoreText.text = score.ToString();
		}

	}

}
