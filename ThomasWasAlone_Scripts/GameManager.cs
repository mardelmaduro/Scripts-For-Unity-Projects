using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	GameObject[] players = new GameObject[2];
	bool player1Active;
	bool player2Active;

	public GameObject ThomasGoal;
	public GameObject ChrisGoal;
	public GameObject level;

	public GameObject thomasArrow;
	public GameObject chrisArrow;

	private bool thomasAtGoal;
	private bool chrisAtGoal;

	// Use this for initialization
	void Start () 
	{
		//players = GameObject.FindGameObjectsWithTag ("Player");

		players [0] = GameObject.FindGameObjectWithTag ("thomas");
		players [1] = GameObject.FindGameObjectWithTag ("chris");
		level = GameObject.FindGameObjectWithTag ("level");

		players [0].SendMessage ("enableControls");
		players [1].SendMessage ("disableControls");

		player1Active = true;
		player2Active = false;

		thomasArrow.SetActive (true);
		chrisArrow.SetActive (false);

		players [0].GetComponentInChildren<Canvas> ().enabled = true;
		players [1].GetComponentInChildren<Canvas> ().enabled = false;
		level.GetComponentInChildren<Text> ().enabled = false;



		thomasAtGoal = false;
		chrisAtGoal = false;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.A) && player1Active == true)
		{
			players [0].SendMessage ("disableControls");
			players [1].SendMessage ("enableControls");
			players [0].GetComponentInChildren<Canvas> ().enabled = false;
			players [1].GetComponentInChildren<Canvas> ().enabled = true;
			thomasArrow.SetActive (false);
			chrisArrow.SetActive (true);
			player1Active = false;
			player2Active = true;
			return;
		}
		if(Input.GetKeyDown(KeyCode.A) && player1Active == false ){
			players [0].SendMessage ("enableControls");
			players [1].SendMessage ("disableControls");
			players [0].GetComponentInChildren<Canvas> ().enabled = true;
			players [1].GetComponentInChildren<Canvas> ().enabled = false;
			thomasArrow.SetActive (true);
			chrisArrow.SetActive (false);
			player1Active = true;
			player2Active = false;
			return;
		}

		if (thomasAtGoal == true && chrisAtGoal == true)
		{
			level.GetComponentInChildren<Text> ().enabled = true;
		}
	}

	void EngageGoalthomas()
	{
		thomasAtGoal = true;
		players [0].GetComponent <SpriteRenderer> ().color = Color.black;
	}

	void DisengageGoalthomas()
	{
		thomasAtGoal = false;
		players [0].GetComponent <SpriteRenderer> ().color = Color.white;
	}

	void EngageGoalchris()
	{
		chrisAtGoal = true;
		players [1].GetComponent <SpriteRenderer> ().color = Color.black;
	}

	void DisengageGoalchris()
	{
		chrisAtGoal = false;
		players [1].GetComponent <SpriteRenderer> ().color = Color.white;
	}
		

}
