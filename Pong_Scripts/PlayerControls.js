﻿#pragma strict

var moveUp : KeyCode;
var moveDown : KeyCode;

var speed: float = 12.5f;

function Update () 
{

	if (Input.GetKey(moveUp))
	{
		GetComponent.<Rigidbody2D>().velocity.y = speed;
	}
	else if (Input.GetKey(moveDown))
	{
		GetComponent.<Rigidbody2D>().velocity.y = speed * -1;
	}
	else
	{
		GetComponent.<Rigidbody2D>().velocity.y = 0;
	}

	GetComponent.<Rigidbody2D>().velocity.x = 0;
}

function OnGUI()
{

}