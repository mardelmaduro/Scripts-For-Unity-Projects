using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour 
{

	public void GoBack()
	{
		SceneManager.LoadScene ("LevelMenu");
	}
}
