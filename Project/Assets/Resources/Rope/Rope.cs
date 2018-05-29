using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Rope : MonoBehaviour {

	static int gRopeNum = 1;

	ObiRopeCursor cursor;
	//ObiDistanceConstraints distance;
	ObiRope rope;

	ObiSolver ropeSolver;

	Transform ropeNode;
	Transform startNode;

	void Awake(){
		cursor = transform.Find ("ObiRope").GetComponentInChildren<ObiRopeCursor>();
		//distance = GetComponentInChildren<ObiDistanceConstraints> ();
		rope = transform.Find ("ObiRope").GetComponentInChildren<ObiRope> ();
		//
		ropeNode = transform.Find ("RopeNode");

		startNode = transform.Find ("StartNode");
	}
	// Use this for initialization
	void Start () {

		/*
		foreach (int x in rope.phases) {
			x = gRopeNum;
		}
		*/
		for (int i = 0; i < rope.phases.Length; i++) {
			rope.phases [i] = gRopeNum;
		}

		gRopeNum++;
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
			 //rope.UVScale = new Vector2 (1, cursor.rope.RestLength);
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
	public void TipeRope(Vector3 point, GameObject fix = null){
		//transform.parent = fix.transform;
		//transform.localPosition = new Vector3(1.7f,0.02f,0);
		//transform.position = point;
		//transform.Find ("StartNode").gameObject.SetActive(true);
		startNode.parent = fix.transform;
		startNode.position = point;

	}

	/// <summary>
	/// 解绳子
	/// </summary>
	/// <param name="point">Point.</param>
	/// <param name="character">Character.</param>
	public void SolutionRope(Vector3 point,GameObject character){
		startNode.parent = character.transform;
		startNode.position = point;
		startNode.gameObject.SetActive(false);
	}

	public void SetSolver(GameObject solverObj){
		if(rope == null)
			rope = transform.Find ("ObiRope").GetComponentInChildren<ObiRope> ();
		rope.Solver = solverObj.GetComponent<ObiSolver> ();
	}
}
