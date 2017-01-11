#pragma strict

 var theBall : Transform;
 var oldPos : int = 0;

 function Start()
 {
 	theBall = GameObject.FindGameObjectWithTag("Ball").transform;
 }

 function MoveUp()
 {
 	if (transform.position.y < 5)
 	{
 	transform.position.y += 0.1/(Random.Range(1,1.1));
 	}
 }

 function MoveDown()
 {
 	if (transform.position.y > -5)
 	{
 	transform.position.y -= 0.1/(Random.Range(1,1.1));
 	}
 }

function Update () 
{
	if(transform.position.y > theBall.transform.position.y)
	{

	MoveDown();

	}

	if(transform.position.y < theBall.transform.position.y)
	{

	MoveUp();

	}

	
}