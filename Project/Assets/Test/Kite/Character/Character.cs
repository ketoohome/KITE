using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	Rope mRope = null;
	Animator mHand = null;
	Transform mBobbinWinder = null;

	float m_TargetRopeLength = 5;
	// 
	void Start(){
		mRope = transform.Find ("Rope").GetComponent<Rope>();
		mHand = transform.Find ("Hand").GetComponent<Animator>();
		mBobbinWinder = transform.Find ("Hand/BobbinWinder/Node");
	}

	// Update is called once per frame
	void Update () {

		mHand.SetInteger ("State",0);

		if (Input.GetMouseButton(2)){
			m_TargetRopeLength += Time.deltaTime * 5;
			mHand.SetInteger ("State",1);
		}

		if (Input.GetAxis ("Mouse ScrollWheel") < 0) 
		{
			m_TargetRopeLength -= 0.3f;
			mHand.SetInteger ("State",1);
		}

		if (Input.GetAxis ("Mouse ScrollWheel") > 0) 
		{
			m_TargetRopeLength += 0.3f;
			mHand.SetInteger ("State",2);
		}

		m_TargetRopeLength = Mathf.Clamp (m_TargetRopeLength,1,200);
		float curr = Mathf.Lerp (mRope.RopeLength,m_TargetRopeLength,Time.deltaTime * 10);

		mRope.RopeLength = curr;
		mBobbinWinder.localRotation = Quaternion.Euler(new Vector3(0,0,-curr *180));

	}
}
