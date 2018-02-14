using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kite : MonoBehaviour {

	Rigidbody rig;
	Vector3 mWind;
	Vector3 mForce;
	// Use this for initialization
	void Start () {
		rig =  GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		mForce = Wind.Instance.WindForce (rig);
		// 风筝的朝向只跟风向有关系
		Debug.Log("速度：" + rig.velocity.magnitude);
	}
}
