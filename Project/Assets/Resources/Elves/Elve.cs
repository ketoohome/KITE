using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elve : MonoBehaviour {

	Transform mKite;

	// Use this for initialization
	void Start () {
		mKite = transform.Find ("Kite");
	}
	
	// Update is called once per frame
	void Update () {
		mKite.LookAt (Camera.main.transform,Vector3.up);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("Kite")) {
			Destroy(gameObject,1f);
			GetComponent<Animator> ().Play ("Destroy");
		}
	}
}
