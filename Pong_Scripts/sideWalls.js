#pragma strict


function OnTriggerEnter2D (hitInfo: Collider2D) 
{
	if(hitInfo.name == "Ball")
	{
		var wallName = transform.name;
		GetComponent.<AudioSource>().Play();
		gameManager.Score(wallName);
		hitInfo.gameObject.SendMessage("ResetBall");
	}


}