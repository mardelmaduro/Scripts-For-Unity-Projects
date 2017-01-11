using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]

public class Tail : MonoBehaviour {

	public float pointSpacing = .1f;

	LineRenderer line;
	EdgeCollider2D col;

	List<Vector2> points;

	float fade = 10;

	public Transform snake;



	void Start () 
	{
		line = GetComponent <LineRenderer> ();
		col = GetComponent <EdgeCollider2D> ();

		points = new List<Vector2> ();

		SetPoint ();
	}
	



	void Update () 
	{
		if (Vector3.Distance (points.Last (), snake.position) > pointSpacing) 
		{
			SetPoint ();
		}
	}



	void SetPoint()
	{
		if (points.Count > 1) {
			
			col.points = points.ToArray <Vector2> ();

		

			if (points.Count > fade) {
				//points.RemoveAt (0);

				for (int i = 0; i < fade; i++) {

					//points.RemoveAll();
					col.points.SetValue(col.points [i + 1], i);
					line.SetPosition (i,line.GetPosition (i + 1));
					if (i < fade && i < 1) {
						points.Remove (points.ElementAt (i));
					}

				}

			}
		}

		points.Add (snake.position);

		line.numPositions = points.Count;

		line.SetPosition ((points.Count - 1), snake.position);



	}

	void setTail()
	{
		fade = GameObject.Find ("Slider").GetComponent<Slider> ().value;
	}


}
