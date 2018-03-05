using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Rope : MonoBehaviour {

	ObiRopeCursor cursor;
	//ObiDistanceConstraints distance;
	ObiRope rope;

	// Use this for initialization
	void Start () {
		cursor = GetComponentInChildren<ObiRopeCursor>();
		//distance = GetComponentInChildren<ObiDistanceConstraints> ();
		rope = GetComponentInChildren<ObiRope> ();
	}

	/*
	float RopeLength{
		get{ return distance.stretchingScale * 5;}
		set{ 
			distance.stretchingScale = value * 0.2f;
			rope.UVScale = new Vector2 (1, distance.stretchingScale * 4);
			distance.PushDataToSolver ();
		}
	}
	*/

	public float RopeLength{
		get{ return cursor.rope.RestLength;}
		set{ cursor.ChangeLength(value);
			 rope.UVScale = new Vector2 (1, cursor.rope.RestLength * 4);
		}
	}

}
