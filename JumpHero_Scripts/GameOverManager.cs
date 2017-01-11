using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

	public static GameOverManager instance;

	private GameObject gameOverPanel;
	private Animator gameOverAnim;


	private Button playAgainBtn, backBtn;

	private GameObject scoreText;
	private Text finalScore;

	// Use this for initialization
	void Awake () 
	{
		MakeInstance ();
		InitializeVariables ();
	}

	void MakeInstance()
	{
		if (instance == null) 
		{
			instance = this;
		}
	}

	public void GameOverShowPanel()
	{
		scoreText.SetActive (false);
		gameOverPanel.SetActive (true);

		finalScore.text = "Score\n" + ScoreManagerScript.instance.GetScore ().ToString ();

		gameOverAnim.Play ("FadeIn");

	}

	void InitializeVariables () 
	{
		gameOverPanel = GameObject.Find ("Game Over Panel Holder");
		gameOverAnim = gameOverPanel.GetComponent <Animator> ();

		playAgainBtn = GameObject.Find ("Restart Button").GetComponent <Button> ();
		backBtn = GameObject.Find ("Back Button").GetComponent <Button> ();

		playAgainBtn.onClick.AddListener (()=>PlayAgain());
		backBtn.onClick.AddListener (()=> BackToMenu());

		scoreText = GameObject.Find ("Score");
		finalScore = GameObject.Find ("Score Text").GetComponent <Text>();

		gameOverPanel.SetActive (false);
	}

	public void PlayAgain()
	{
		gameObject.GetComponent <AudioSource>().Play ();
		SceneManager.LoadScene ("GamePlay");
	}

	public void BackToMenu()
	{
		gameObject.GetComponent <AudioSource>().Play ();
		SceneManager.LoadScene ("MainMenu");
	}
}
