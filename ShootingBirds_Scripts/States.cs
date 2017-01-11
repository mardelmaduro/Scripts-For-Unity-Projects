using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlingShotState
{
	Idle,
	UserPulling,
	BirdFlying
}

public enum GameState
{
	Start,
	BirdMovingToSlingshot,
	Playing,
	Won,
	Lost
}

public enum BirdState
{
	BeforeThrown,
	Thrown
}
