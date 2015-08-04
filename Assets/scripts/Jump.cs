using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public float intensity;//Intensity of the jump.
	private bool grounded;//Indicates if the player touches the ground.
	public bool doubleJump;//Indicates if the player is alowed to double jump.
	public bool jump;//Indicates if te palyer is alowed to jump
	public bool doubleJumped;//Indicates if the player double jumped.

	private Rigidbody rb;//Contains the rigidybody component;

	//Gets the rigidybody of the sphere.
	void Start(){
		rb = GetComponent<Rigidbody> ();
	}


	void Update(){
		SetGrounded ();

		if (jump && Input.GetButtonDown ("Jump")) {
			if(grounded){
				doJump (intensity);
			}else{
				if(doubleJump && !doubleJumped){
					doJump (intensity);
					doubleJumped = true;
				}
			}
		}
	}

	/*Casts a ray a little bit larger than the radio of the sphere collider on axis -y and
	sets grounded and doubleJumped as true if it hits a surface.*/
	void SetGrounded(){
		if(Physics.Raycast(transform.position, new Vector3(0, -1, 0), 0.51f)){
			grounded = true;
			doubleJumped = false;
		}
		else{
			grounded = false;
		}
	}

	//Adds force to +y axis
	public void doJump(float intensity){
		rb.AddForce(new Vector3(0.0f, intensity, 0.0f));
	}

}
