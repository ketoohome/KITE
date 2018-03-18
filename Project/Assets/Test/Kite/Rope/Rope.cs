using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Rope : MonoBehaviour {

	ObiRopeCursor cursor;
	//ObiDistanceConstraints distance;
	ObiRope rope;

	Transform ropeNode;

	// Use this for initialization
	void Start () {
		cursor = transform.Find ("ObiRope").GetComponentInChildren<ObiRopeCursor>();
		//distance = GetComponentInChildren<ObiDistanceConstraints> ();
		rope = transform.Find ("ObiRope").GetComponentInChildren<ObiRope> ();
		//
		ropeNode = transform.Find ("RopeNode");
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

	/// <summary>
	/// Gets or sets the length of the rope.
	/// </summary>
	/// <value>The length of the rope.</value>
	public float RopeLength{
		get{ return cursor.rope.RestLength;}
		set{ cursor.ChangeLength(value);
			 rope.UVScale = new Vector2 (1, cursor.rope.RestLength * 4);
		}
	}

	/// <summary>
	/// Gets the end point.
	/// </summary>
	/// <returns>The end point.</returns>
	public Vector3 GetEndPoint(){
		return rope.GetParticlePosition(rope.TotalParticles-1);
	}

	/// <summary>
	/// 链接风筝
	/// </summary>
	/// <param name="kite">Kite.</param>
	public void ConnectKite(GameObject kite){
		gameObject.SetActive (true);
		Transform connectPoint = kite.transform.Find ("ConnectPoint");
		ropeNode.position = connectPoint.position;
		ropeNode.transform.parent = kite.transform.parent;
		kite.GetComponent<FixedJoint>().connectedBody = ropeNode.GetComponent<Rigidbody>();
	}

	/// <summary>
	/// 断开风筝的链接，并绑定在一个物体上
	/// </summary>
	/// <param name="fix">Fix.</param>
	public void DisconnectKite(Vector3 point, GameObject fix = null){
		transform.parent = fix.transform;
		//transform.localPosition = new Vector3(1.7f,0.02f,0);
		transform.position = point;
	}
}
