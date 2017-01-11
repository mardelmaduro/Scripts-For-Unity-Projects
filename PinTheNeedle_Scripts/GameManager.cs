using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	private Button shootBtn;

	[SerializeField]
	private GameObject needle;

	private GameObject[] gameNeedles;

	[SerializeField]
	private int howManyNeedles;

	private float needleDistance = 0.5f;
	private int needleIndex;

	private bool flying = false;
	private bool gameOver = false;



	void Awake () 
	{
		gameOver = false;
		if (instance == null) 
		{
			instance = this;
		}

		GetButton ();
	}

	void Start ()
	{
		CreateNeedles ();
	}

	void GetButton()
	{
		shootBtn = GameObject.Find ("Shoot Button").GetComponent <Button>();

		//shootBtn.onClick.AddListener (() => ShootTheNeedle ());
	}

	public void ShootTheNeedle()
	{
		if (!flying) 
		{
			flying = true;
			gameNeedles [needleIndex].GetComponent <NeedleMovementScript> ().FireTheNeedle ();
			needleIndex++;
			flying = false;
		}

		if (needleIndex == gameNeedles.Length) 
		{
			shootBtn.onClick.RemoveAllListeners ();
			shootBtn.gameObject.SetActive (false);
			StartCoroutine (GameOver ());


		}

	}

	void CreateNeedles()
	{
		gameNeedles = new GameObject[howManyNeedles];

		Vector3 temp = transform.position;

		for (int i = 0; i < gameNeedles.Length; i++) 
		{
			gameNeedles[i] = Instantiate (needle, temp, Quaternion.identity) as GameObject;
			temp.y -= needleDistance;
			gameNeedles [i].SetActive (true);
		}

	}

	public void InstantiateNeedle()
	{
		Instantiate (needle, transform.position, Quaternion.identity);
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
