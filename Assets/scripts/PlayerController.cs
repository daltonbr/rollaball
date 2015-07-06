using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed;

	private Rigidbody playerRB;
    private int count;

    public GameController gameController;
	
	void Start ()
	{
  		playerRB = GetComponent<Rigidbody>();
    }
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");	
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		playerRB.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("PickUp") )
		{
            gameController.UpdatePickUpCount(); // atualiza a contagem de PickUps        
            other.gameObject.SetActive(false);  // desativa o pickUp
            gameController.UpdatePickUpCount();  // conta pickUps restantes
        }
    }

}