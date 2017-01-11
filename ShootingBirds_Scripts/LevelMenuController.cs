using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuController : MonoBehaviour {

	public void PlayLevel1()
	{
		SceneManager.LoadScene ("GamePlay");
	}

	public void PlayLevel2()
	{
		SceneManager.LoadScene ("GamePlay2");
	}

	public void PlayLevel3()
	{
		SceneManager.LoadScene ("GamePlay3");
	}

	public void GoBack()
	{
		SceneManager.LoadScene ("MainMenu");
	}
}
