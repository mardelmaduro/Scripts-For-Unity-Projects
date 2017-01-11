using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NeedleHeadScript : MonoBehaviour {

	private Button shootBtn;

	void Awake()
	{
		if (SceneManager.GetActiveScene ().name != "MainMenu") 
		{
			shootBtn = GameObject.Find ("Shoot Button").GetComponent <Button> ();
		}
	}

	void OnTriggerEnter2D(Collider2D target)
	{
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
