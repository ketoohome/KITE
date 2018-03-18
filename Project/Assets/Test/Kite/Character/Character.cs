using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	Rope mRope = null;
	Animator mHand = null;
	Transform mBobbinWinder = null;

	float m_TargetRopeLength = 1;
	// 
	void Start(){
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

		if (Input.GetMouseButtonDown (0)) {
			PickUpKite ();
		}

		if (Input.GetMouseButtonDown (1)) {
			FixRope ();
		}

		if (mRope != null) {
			m_TargetRopeLength = Mathf.Clamp (m_TargetRopeLength,0,200);
			float curr = Mathf.Lerp (mRope.RopeLength,m_TargetRopeLength,Time.deltaTime * 10);

			mRope.RopeLength = curr;
			mBobbinWinder.localRotation = Quaternion.Euler(new Vector3(0,0,-curr *180));
		}

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			if(mRope == null) CreateRope("Rope_01");
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			if(mRope == null) CreateRope("Rope_02");
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			if(mRope == null) CreateRope("Rope_03");
		}
	}

	/// <summary>
	/// 拾取风筝
	/// </summary>
	void PickUpKite(){
		if (mRope == null) return;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 3)) {
			Kite kite = hit.collider.transform.GetComponent<Kite> ();
			if (kite != null) mRope.ConnectKite (hit.collider.gameObject);
		}
	}

	/// <summary>
	/// 固定风筝线
	/// </summary>
	void FixRope(){
		if (mRope == null) return;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 3)) {
			mRope.DisconnectKite (hit.point,hit.collider.gameObject);
			mRope = null;
		}
	}

	/// <summary>
	/// 更换绳子
	/// </summary>
	void CreateRope(string name){
		m_TargetRopeLength = 0.5f;
		GameObject obj = Instantiate<GameObject> (Resources.Load<GameObject> (name),transform.position,transform.rotation,transform);
		mRope = obj.GetComponent<Rope> ();
	}
}
