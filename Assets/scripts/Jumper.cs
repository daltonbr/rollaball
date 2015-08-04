using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {

	public float intensity;

	void OnTriggerStay(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			other.GetComponent<Jump>().doJump (intensity);
		}
	}
}
