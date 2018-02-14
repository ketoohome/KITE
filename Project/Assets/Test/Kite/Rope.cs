using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Rope : MonoBehaviour {

	ObiRopeCursor cursor;
	ObiDistanceConstraints distance;
	ObiRope rope;
	//ObiDistanceConstraintBatch distance;

	// Use this for initialization
	void Start () {
		cursor = GetComponentInChildren<ObiRopeCursor>();
		distance = GetComponentInChildren<ObiDistanceConstraints> ();
		rope = GetComponentInChildren<ObiRope> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton(2)){
			m_TargetRopeLength += Time.deltaTime * 5;
		}

		if (Input.GetAxis ("Mouse ScrollWheel") < 0) 
		{
			m_TargetRopeLength -= 0.1f;
		}

		if (Input.GetAxis ("Mouse ScrollWheel") > 0) 
		{
			m_TargetRopeLength += 0.1f;
		}

		m_TargetRopeLength = Mathf.Clamp (m_TargetRopeLength,1,50);
		if(Mathf.Abs(RopeLength - m_TargetRopeLength) >0.01f)
			RopeLength = Mathf.Lerp (RopeLength,m_TargetRopeLength,Time.deltaTime * 10);
	}


	float m_TargetRopeLength = 5;
	float RopeLength{
		get{ return distance.stretchingScale * 5;}
		set{ 
			distance.stretchingScale = value * 0.2f;
			rope.UVScale = new Vector2 (1, distance.stretchingScale * 4);
			distance.PushDataToSolver ();
		}
	}

}
