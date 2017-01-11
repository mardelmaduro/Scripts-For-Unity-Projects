#pragma strict

var ballSpeed : float = 150;
var ball : Rigidbody2D;

	ball = GetComponent.<Rigidbody2D>();

var click : AudioSource;
	click = GetComponent.<AudioSource>();

function Start () 
{
	//yield WaitForSeconds (3);
	//GoBall();

}

function Update()
{
	var xVel : float = GetComponent.<Rigidbody2D>().velocity.x;

	if (xVel < 18 && xVel > -18 && xVel != 0)
	{
		if(xVel > 0)
		{
			GetComponent.<Rigidbody2D>().velocity.x = 20;
		}else{
			GetComponent.<Rigidbody2D>().velocity.x = -20;
		}
//	Debug.Log ("Velocity Before" + xVel);
//	Debug.Log("Velocity After" + GetComponent.<Rigidbody2D>().velocity.x);
	}

}

function OnCollisionEnter2D (colInfo : Collision2D) 
{
	
	if(colInfo.collider.tag == "Player")
	{
	var velY = GetComponent.<Rigidbody2D>().velocity.y/3 + colInfo.collider.GetComponent.<Rigidbody2D>().velocity.y/2;
	GetComponent.<Rigidbody2D>().velocity.y = velY;
	click.pitch = Random.Range(0.3f, 1.2f);
	click.Play();
 	}

}


function ResetBall()
{
	ball.velocity.x = 0;
	ball.velocity.y = 0;
	transform.position.x = 0;
	transform.position.y = 0;

	yield WaitForSeconds(2);
	GoBall();
}



function GoBall()
{
var randomNumber = Random.Range(0,2);
	



	if (randomNumber <= 0.5)
	{
	ball.AddForce(new Vector2 (ballSpeed, 10));
	}else{
	ball.AddForce(new Vector2 (-ballSpeed, -10));
	}
}



