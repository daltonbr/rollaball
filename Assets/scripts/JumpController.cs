﻿using UnityEngine;
using System.Collections;

public class JumpController : MonoBehaviour {
	public float intensity = 200;
	bool isGrounded = true;
	bool hasDoubleJumped = false;
	Rigidbody playerRB;

	void Awake(){
		playerRB = GetComponent<Rigidbody>()  ;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (isGrounded + "\n" + hasDoubleJumped);
		if (Input.GetButtonDown ("Jump")){
			if(isGrounded || !hasDoubleJumped){
				Jump();
			}
		}
	}

	void Jump(){
		playerRB.AddForce (transform.up * intensity);
		if (isGrounded)
			isGrounded = false;
		else
			hasDoubleJumped = true;
	}
}
