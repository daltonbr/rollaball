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
       // gameController = FindObjectOfType<GameController>();  // search for the GameController
    }
    //OnLevelWasLoaded is called after a new scene has finished loading
    void OnLevelWasLoaded()
    {
        //  Debug.Log("Scene Loaded");   
    }


    void Awake()
    {
        //Get a reference to GameController -  attached to UI object
        //gameController = GetComponent<GameController>();
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
            //gameController.UpdatePickUpCount(); // atualiza a contagem de PickUps    
            //other.gameObject.SetActive(false);  // desativa o pickUp
            //gameController.UpdatePickUpCount();  // conta pickUps restantes

			LevelController.Instance.OnPickUpGet(other.gameObject);
        }
    }

}