
using UnityEngine;
//using UnityEngine.iOS;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using System.Collections;
//using UnityEditor;

public class Snake : MonoBehaviour {

	public float speed = 3f;
	public float rotationSpeed = 250f;
	private float tailLength = 10f;
	int Score1 = 0;
	int Score2 = 0;
	int passes = 0;
	public ParticleSystem deathParticles1;
	public ParticleSystem deathParticles2;


	public string inputAxis = "Horizontal";
	//public AudioSource beat;

	float horizontal = 0f;
	private Vector3 startPos;
	private Quaternion startRot;
	//private LineRenderer linePos;
	//private EdgeCollider2D lineCol;
	private GameObject [] temp;



	void Start()
	{
		//beat = GameObject.Find ("GameManager").GetComponent<AudioSource> ();
		startPos = transform.position;
		startRot = transform.rotation;
		//linePos = transform.parent.GetComponentInChildren<LineRenderer>();
		//lineCol = transform.parent.GetComponentInChildren<EdgeCollider2D>();
	}

	void Update () 
	{

		horizontal = Input.GetAxisRaw(inputAxis);

		if (transform.parent.GetComponentInChildren<EdgeCollider2D>().enabled == false && passes >= tailLength) 
		{
			reEnableCollider();

			passes = 0;
		}
		passes++;
		checkWin ();
	}

	void FixedUpdate()
	{
		transform.Translate (Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
		transform.Rotate (Vector3.forward * -horizontal * rotationSpeed * Time.fixedDeltaTime);


	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//ParticleSystem death  = (ParticleSystem) Instantiate(deathParticles,gameObject.transform.position, gameObject.transform.localRotation);

		if(transform.parent.name.Equals ("Player1"))
		{
			ParticleSystem death  = (ParticleSystem) Instantiate(deathParticles1,gameObject.transform.position, gameObject.transform.localRotation);
			death.Play ();
		}

		if(transform.parent.name.Equals ("Player2"))
		{
			ParticleSystem death  = (ParticleSystem) Instantiate(deathParticles2,gameObject.transform.position, gameObject.transform.localRotation);
			death.Play ();
		}



		if (col.tag == "KillsPlayer" && GetComponent <AudioSource> ().isPlaying == false) {
			
			speed = 0f;
			rotationSpeed = 0f;

			GetComponent <AudioSource> ().Play ();





			transform.parent.GetComponentInChildren <EdgeCollider2D> ().enabled = false;
			transform.parent.GetComponentInChildren<LineRenderer> ().enabled = false;



			if (transform.parent.name == "Player1" && Score1 < 5 && Score2 < 5) {
				//resetPosition ();
				GameObject.FindObjectOfType<Gamemanager> ().resetBoard ();
				Score2++;
				//Debug.Log (Score2);
				GameObject.Find ("player2Score").GetComponent<Text> ().text = Score2.ToString ();
				//transform.parent.GetComponentInChildren<LineRenderer> ().enabled = true;
				//checkWin ();
				return;

			} 

			if (transform.parent.name == "Player2" && Score1 < 5 && Score2 < 5) {
				//resetPosition ();
				GameObject.FindObjectOfType<Gamemanager> ().resetBoard ();
				Score1++;
				//Debug.Log (Score1);
				GameObject.Find ("player1Score").GetComponent<Text> ().text = Score1.ToString ();
				//transform.parent.GetComponentInChildren<LineRenderer> ().enabled = true;
				//checkWin ();
				return;
			} 
			//Debug.Log ("Killed by" + col.name);
			//checkWin ();

		}
	}

	public void resetPosition()
	{

		transform.parent.GetComponentInChildren <EdgeCollider2D>().enabled = false;
		transform.parent.GetComponentInChildren<LineRenderer> ().enabled = false;
		transform.position = startPos;
		transform.rotation = startRot;

	
		for (int x = 0; x < transform.parent.GetComponentInChildren <LineRenderer> ().numPositions; x++) 
		{
			transform.parent.GetComponentInChildren <LineRenderer> ().SetPosition(x, startPos);
		}
			
		transform.parent.GetComponentInChildren<LineRenderer> ().enabled = true;

		//transform.parent.GetComponentInChildren<EdgeCollider2D> ().isTrigger = true;
		speed = 3f;
		rotationSpeed = 250f;


		//transform.parent.GetComponentInChildren<EdgeCollider2D> ().enabled = true;

	}

	public void reEnableCollider()
	{
		//transform.parent.GetComponentInChildren<EdgeCollider2D> ().Reset ();
		transform.parent.GetComponentInChildren<EdgeCollider2D> ().enabled = true;
	}

	public void checkWin()
	{
		if ( Score1 > 4  && GameObject.Find ("player2Score").GetComponent<Text> ().text.Equals (GameObject.Find ("player1Score").GetComponent<Text> ().text)) {
			GameObject.Find ("Title (1)").GetComponent<Text> ().color = new Color32 (196, 25, 223, 255);
			GameObject.Find ("Title (1)").GetComponent<Text> ().text = "draw";
			//GameObject.Find ("Player1").SetActive (false);
			//GameObject.Find ("Player2").SetActive (false);
			GameObject.FindObjectOfType<Gamemanager>().EndGame();
			return;
		}



		if (Score1 > 4 && Score1 > Score2 && Score1 != Score2 && GameObject.Find ("Title (1)").GetComponent<Text> ().text.Equals ("")) {
			GameObject.Find ("Title (1)").gameObject.SetActive (true);
			GameObject.Find ("Title (1)").GetComponent<Text> ().color = new Color32 (43, 202, 255, 255);
			GameObject.Find ("Title (1)").GetComponent<Text> ().text = "blue wins";
			GameObject.FindObjectOfType<Gamemanager>().EndGame();
			return;
		}



		if (Score2 > 4 && Score1 < Score2 && Score1 != Score2 && GameObject.Find ("Title (1)").GetComponent<Text> ().text.Equals ("")) {
			GameObject.Find("Title (1)").gameObject.SetActive (true);
			GameObject.Find ("Title (1)").GetComponent<Text> ().color = new Color32 (255, 65, 43, 255);
			GameObject.Find ("Title (1)").GetComponent<Text> ().text = "red wins";
			GameObject.FindObjectOfType<Gamemanager>().EndGame();
			return;
		}


	}


	IEnumerator quickWait()
	{
		yield return new WaitForSeconds (0.5f);

	}
}
