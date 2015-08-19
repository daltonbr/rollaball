using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

	public GameController gameController;

	//If the player gets out of the boundary...
	void OnTriggerExit(Collider other){
		if (other.tag=="Player"){
			//Die function is called.
			gameController.Die();
		}
	}
}
