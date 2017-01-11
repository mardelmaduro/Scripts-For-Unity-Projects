using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NeedleMovementScript : MonoBehaviour {

	[SerializeField]
	private GameObject needleBody;	

	private Button shootBtn;

	private bool canFireNeedle;
	private bool touchedTheCircle;
	private bool needleInAir;

	private float forceY = 20f;

	private Rigidbody2D myBody;

	// Use this for initialization
	void Awake () 
	{
		Initialize ();
		if (SceneManager.GetActiveScene ().name != "MainMenu") 
		{
			shootBtn = GameObject.Find ("Shoot Button").GetComponent <Button> ();
		}

	}
	
	// Update is called once per frame
	void Initialize () 
	{
		needleBody.SetActive (false);	
		myBody = GetComponent < Rigidbody2D> ();
	}

	void Update()
	{
		if (canFireNeedle) 
		{
			myBody.velocity = new Vector2 (0, forceY);
		}
	}

	public void FireTheNeedle()
	{
		if (!needleInAir) 
		{
			needleInAir = true;
			needleBody.SetActive (true);
			myBody.isKinematic = false;
			canFireNeedle = true;
		}
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (touchedTheCircle) {
			return;
		} 

		if (target.tag == "Circle") 
		{
			gameObject.GetComponent <AudioSource>().Play();
			canFireNeedle = false;
			needleInAir = false;
			touchedTheCircle = true;

			myBody.isKinematic = true;
			myBody.velocity = Vector2.zero;
			gameObject.transform.SetParent (target.transform);

			this.tag = "Needle Head";

			if (ScoreManager.instance != null) 
			{
				ScoreManager.instance.setScore ();
			}

		}
		if (target.tag == "Needle Head") 
		{
			shootBtn.onClick.RemoveAllListeners ();
			shootBtn.gameObject.SetActive (false);
			StartCoroutine (GameOver ());
			Time.timeScale = 1f;
		}
	}

	IEnumerator GameOver()
	{

		Time.timeScale = 0.5f;
		yield return new WaitForSeconds (4f);
		SceneManager.LoadScene ("MainMenu");
		yield return new WaitForSeconds (4f);
		Time.timeScale = 1f;
	}
}
