using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour {

	private Vector3 slingShotMiddleVector;

	[HideInInspector]
	public SlingShotState slingShotState;

	public Transform leftSlingShotOrigin,rightSlingShotOrigin;

	public LineRenderer slingShotLineRenderer1 ,slingShotLineRenderer2 , trajectoryLineRenderer;

	[HideInInspector]
	public GameObject birdToThrow;

	public Transform birdWaitPosition;

	public float throwSpeed;

	[HideInInspector]
	public float timeSinceThrown;

	public delegate void BirdThrown ();
	public event BirdThrown birdThrown;



	void Awake () 
	{
		slingShotLineRenderer1.sortingLayerName = "Foreground";	
		slingShotLineRenderer2.sortingLayerName = "Foreground";	
		trajectoryLineRenderer.sortingLayerName = "Foreground";	

		slingShotState = SlingShotState.Idle;

		slingShotLineRenderer1.SetPosition (0,leftSlingShotOrigin.position);
		slingShotLineRenderer2.SetPosition (0,rightSlingShotOrigin.position);

		slingShotMiddleVector = new Vector3 ((leftSlingShotOrigin.position.x + rightSlingShotOrigin.position.x) / 2,(leftSlingShotOrigin.position.y + rightSlingShotOrigin.position.y) / 2, 0);


	}

	void Update()
	{
		switch (slingShotState) 
		{
		case SlingShotState.Idle:
			InitializeBird ();
			displaySlingshotLineRenderers ();

			if (Input.GetMouseButtonDown (0)) {
				Vector3 location = Camera.main.ScreenToWorldPoint (Input.mousePosition);

				if (birdToThrow.GetComponent <CircleCollider2D> () == Physics2D.OverlapPoint (location)) {
					slingShotState = SlingShotState.UserPulling;
				}
			}
			break;
		case SlingShotState.UserPulling:

			displaySlingshotLineRenderers ();

			if (Input.GetMouseButton (0)) {
				Vector3 location = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				location.z = 0;

				if (Vector3.Distance (location, slingShotMiddleVector) > 2f) {
					var maxPosition = (location - slingShotMiddleVector).normalized * 2f + slingShotMiddleVector;

					birdToThrow.transform.position = maxPosition;
				}
				else 
				{
					birdToThrow.transform.position = location;
				}

				var distance = Vector3.Distance (slingShotMiddleVector, birdToThrow.transform.position);
				displayTrajectoryLineRendererActive (distance);
			} 
			else 
			{
				setTrajectoryLineRendererActive (true);
				timeSinceThrown = Time.time;
				float distance =  Vector3.Distance (slingShotMiddleVector, birdToThrow.transform.position);
				if (distance > 1) {
					setSlingshotLineRenderersActive (false);
					slingShotState = SlingShotState.BirdFlying;
					throwBird (distance);
				} 
				else 
				{
					birdToThrow.transform.positionTo (distance/10, birdWaitPosition.position);
					InitializeBird ();
				}
			}

			break;
		}
	}
	

	private void InitializeBird() 
	{
		birdToThrow.transform.position = birdWaitPosition.position;

		slingShotState = SlingShotState.Idle;

		setSlingshotLineRenderersActive(true);
	}

	void setSlingshotLineRenderersActive(bool active)
	{
		slingShotLineRenderer1.enabled = active;
		slingShotLineRenderer2.enabled = active;
	}

	void displaySlingshotLineRenderers()
	{
		slingShotLineRenderer1.SetPosition (1,birdToThrow.transform.position);
		slingShotLineRenderer2.SetPosition (1,birdToThrow.transform.position);
	}

	void setTrajectoryLineRendererActive (bool active)
	{
		trajectoryLineRenderer.enabled = active;
	}

	void displayTrajectoryLineRendererActive (float distance)
	{
		setTrajectoryLineRendererActive (true);

		Vector3 v2 = slingShotMiddleVector - birdToThrow.transform.position;

		int segmentCount = 15;

		Vector2 [] segments = new Vector2[segmentCount];

		segments [0] = birdToThrow.transform.position;


		Vector2 segVelocity = new Vector2 (v2.x, v2.y)*throwSpeed*distance;


		for (int i = 1; i < segmentCount; i++)
		{
			float time = i * Time.fixedDeltaTime * 5f;
			segments [i] = segments [0] + segVelocity * time + 0.5f * Physics2D.gravity * Mathf.Pow (time, 2);
		}

		trajectoryLineRenderer.numPositions = segmentCount;

		for (int j = 0; j < segmentCount; j++) 
		{
			trajectoryLineRenderer.SetPosition (j,segments[j]);
		}
	}

	private void throwBird(float distance)
	{
		Vector3 velocity = slingShotMiddleVector - birdToThrow.transform.position;

		birdToThrow.GetComponent <Bird>().onThrow ();

		birdToThrow.GetComponent <Rigidbody2D> ().velocity = new Vector2 (velocity.x, velocity.y) * throwSpeed * distance;

		if (birdThrown != null)
			birdThrown ();



	}
}
