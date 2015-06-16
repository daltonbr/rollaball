using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    void FixedUpdate() {
    
     		float moveHorizontal = Input.GetAxis("Horizontal");
    		float moveVertical = Input.GetAxis("Vertical");
			//Debug.Log ("Horizontal: " + moveHorizontal + "Vertical: " + moveVertical);	

    		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);  
    		
    		GetComponent<Rigidbody>().AddForce (movement);
    }
	
}
