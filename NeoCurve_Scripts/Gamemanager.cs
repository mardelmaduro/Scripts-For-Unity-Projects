using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour {

	private bool hasEnded = false;
	private GameObject player1;
	private GameObject player2;

	//Canvas UI;
	Text title;
	Text titleOutliner;
	Text score1;
	Text score2;
	Button playButton;
	GameObject[] tails;
	Slider slide;
	int p;

	private GameObject[] players;

	public void Start()
	{
		//player1 = GameObject.Find ("Player1");
		//player2 = GameObject.Find ("Player2");
		players = GameObject.FindGameObjectsWithTag ("Player");

		player1 = players [0];
		player2 = players [1];

		//UI = (Canvas) GameObject.Find ("Canvas").GetComponent<Canvas>();
		title = GameObject.Find ("Title").GetComponent<Text>();
		titleOutliner = GameObject.Find ("Title (1)").GetComponent<Text>();
		score1 = GameObject.Find ("player1Score").GetComponent<Text>();
		score2 = GameObject.Find ("player2Score").GetComponent<Text>();
		playButton = GameObject.Find ("playButton").GetComponent<Button>();
		tails = GameObject.FindGameObjectsWithTag ("KillsPlayer");



		slide = GameObject.Find ("Slider").GetComponent <Slider> ();
		score1.gameObject.SetActive (false);
		score2.gameObject.SetActive (false);
		player1.gameObject.SetActive(false);
		player2.gameObject.SetActive(false);

	}

	public void EndGame()
	{
		if (hasEnded)
			return;
		Time.timeScale = 0.25f;
		hasEnded = true;
		StartCoroutine (PlayEndGameAnimation());
	}

	IEnumerator PlayEndGameAnimation()
	{
		Debug.Log ("GAME OVER");

		yield return new WaitForSeconds (0.5f);
		////GameObject.Find ("Player1").SetActive (false);
		GameObject.Find ("Player1").SetActive (false);
		yield return new WaitForSeconds (0.5f);

		Time.timeScale = 1f;


		SceneManager.LoadScene ((SceneManager.GetActiveScene ().buildIndex));
	}

	IEnumerator resetCall()
	{
		yield return new WaitForSeconds (0.5f);

	}

	public void pressedPlay()
	{
		//UI.gameObject.SetActive (false);

		for (int i = 0; i < tails.Length; i++) 
		{
			if (tails [i].name == "Tail") 
			{
				tails [i].SendMessage ("setTail");
			}
		}

		title.gameObject.SetActive (false);
		titleOutliner.gameObject.GetComponent<Text>().text = "";
		playButton.gameObject.SetActive (false);
		slide.gameObject.SetActive (false);
		player1.gameObject.SetActive(true);
		player2.gameObject.SetActive(true);
		score1.gameObject.SetActive (true);
		score2.gameObject.SetActive (true);

	}

	public void resetBoard()
	{
			resetCall ();

			player1.SendMessage ("resetPosition");
			player2.SendMessage ("resetPosition");

			resetCall ();
	}
}
