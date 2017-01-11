using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public CameraFollow cameraFollow;

	int currentBirdIndex;

	public SlingShot slingShot;

	[HideInInspector]
	public static GameState gameState;

	private List <GameObject> bricks;
	private List <GameObject> birds;
	private List <GameObject> pigs;




	void Awake () 
	{
		gameState = GameState.Start;
		slingShot.enabled = false;

		slingShot.slingShotLineRenderer1.enabled = false;
		slingShot.slingShotLineRenderer2.enabled = false;

		bricks = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Brick"));
		birds = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Bird"));
		pigs = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Pig"));

	}

	void OnEnable()
	{
		slingShot.birdThrown += slingShotBirdThrown;
	}

	void OnDisable()
	{
		slingShot.birdThrown -= slingShotBirdThrown;
	}

	void Update () 
	{
		switch (gameState) 
		{

		case GameState.Start:
			if(Input.GetMouseButtonUp (0))
				{
					animateBirdToSlingShot ();
				}
			break;

		case GameState.Playing:
			if (slingShot.slingShotState == SlingShotState.BirdFlying && (bricksBirdsPigsStoppedMoving () || Time.time - slingShot.timeSinceThrown > 5f)) {

				slingShot.enabled = false;
				slingShot.slingShotLineRenderer1.enabled = false;
				slingShot.slingShotLineRenderer2.enabled = false;

				animateCameraToStartPosition ();
				gameState = GameState.BirdMovingToSlingshot;
			}
			break;
		case GameState.Won:
			SceneManager.LoadScene ("WinScene");
			break;

		case GameState.Lost:
			SceneManager.LoadScene ("LoseScene");
			break;




		}
	}

	void animateBirdToSlingShot()
	{
		gameState = GameState.BirdMovingToSlingshot;
		birds[currentBirdIndex].transform.positionTo (Vector2.Distance (birds[currentBirdIndex].transform.position/10,
																			slingShot.birdWaitPosition.position)/10,
																			slingShot.birdWaitPosition.position).
		setOnCompleteHandler ((x) =>  {
			x.complete ();
			x.destroy ();

			gameState = GameState.Playing;
			slingShot.enabled = true;

			slingShot.birdToThrow = birds[currentBirdIndex];
		});
	}

	bool bricksBirdsPigsStoppedMoving()
	{
		foreach (var item in bricks.Union (birds).Union (pigs)) 
		{
			if (item != null && item.GetComponent <Rigidbody2D> ().velocity.sqrMagnitude > GameVariables.minVelocity)
				return false;
		}


		return true;
	}


	private bool allPigsAreDestroyed()
	{
		return pigs.All (x => x == null);
	}

	private void animateCameraToStartPosition()
	{
		float duration = Vector2.Distance (Camera.main.transform.position, cameraFollow.startingPosition) / 10f;

		if (duration == 0.0f)
			duration = 0.1f;

		Camera.main.transform.positionTo (duration, cameraFollow.startingPosition).
		setOnCompleteHandler ((x) => {
			cameraFollow.isFollowing = false;
			if(allPigsAreDestroyed ())
			{
				gameState = GameState.Won;
			}
			else if (currentBirdIndex == birds.Count -1)
			{
				gameState = GameState.Lost;
			}
			else
			{
				slingShot.slingShotState = SlingShotState.Idle;
				currentBirdIndex++;
				animateBirdToSlingShot ();
			}
		});
	}

	private void slingShotBirdThrown()
	{
		cameraFollow.birdToFollow = birds [currentBirdIndex].transform;
		cameraFollow.isFollowing = true;

	}

	public void backToLevelMenu()
	{
		if (gameState == GameState.Start) 
		{
			gameState = GameState.Playing;
		}


		SceneManager.LoadScene ("LevelMenu");


	}

	public void resetLevel()
	{
		if (gameState == GameState.Start) 
		{
			gameState = GameState.Playing;
		}


		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);


	}
}
