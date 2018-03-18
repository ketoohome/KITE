using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Kite.
/// </summary>
public class Kite : MonoBehaviour {

	Rigidbody mRig;
	// Use this for initialization
	void Start () {
		mRig =  GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Wind.Instance.WindForce (mRig);
		// 风筝的朝向只跟风向有关系
		//Debug.Log("速度：" + rig.velocity.magnitude);
	}
}
