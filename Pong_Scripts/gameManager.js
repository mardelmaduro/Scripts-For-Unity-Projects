#pragma strict
import UnityEngine.SceneManagement;

 static var playerScore01 : int = 0;
 static var playerScore02 : int = 0;

 var theSkin : GUISkin;

 var theBall : Transform;

 function Start()
 {
 	theBall = GameObject.FindGameObjectWithTag("Ball").transform;
 	GameObject.FindGameObjectWithTag("Ball").GetComponent.<SpriteRenderer>().color.a = 0;
 }


 static function Score(wallName : String) 
{
	if(wallName == "rightWall")
	{
	playerScore01 += 1;
	}else{
	playerScore02 += 1;
	}

	if(playerScore01 == 5)
	{
		playerScore01 = 0;
		playerScore02 = 0;
		SceneManager.LoadScene("MainLevel");

	}
	if(playerScore02 == 5)
	{
		playerScore01 = 0;
		playerScore02 = 0;
		SceneManager.LoadScene("MainLevel");
	}

}

function OnGUI ()
{
	GUI.skin = theSkin;
	GUI.Label(new Rect (Screen.width/2-150-18, 30, 100, 100), "" + playerScore01);
	GUI.Label(new Rect (Screen.width/2+150-18, 30, 100, 100), "" + playerScore02);

	if(GUI.Button ( new Rect (Screen.width/2-121/2, 35, 121, 53), "Reset"))
	{
		playerScore01 = 0;
		playerScore02 = 0;
		theBall.gameObject.SendMessage ("ResetBall");
	}

}

function ButtonPress()
{
    //yield WaitForSeconds (3);
	theBall.gameObject.SendMessage ("GoBall");
	GameObject.Find("Canvas/Button").SetActive(false);
	GameObject.Find("Canvas/GameTitle").SetActive(false);
	GameObject.FindGameObjectWithTag("Ball").GetComponent.<SpriteRenderer>().color.a = 1;

}

function Reset()
{
	playerScore01 = 0;
	playerScore02 = 0;
	theBall.gameObject.SendMessage ("ResetBall");
}






