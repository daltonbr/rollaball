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
 
    void Awake()
    {
        //Get a reference to GameController -  attached to GameController (empty GameObject)
        gameController = FindObjectOfType<GameController>();  // search for the GameController
        if (gameController == null ) // in case of the scene was loaded directly (Editor)
        {
            Debug.Log("GameController not founded! Instantiating one!");
            GameObject _GameController = Instantiate(Resources.Load("prefabs/GameController", typeof(GameObject))) as GameObject;
            gameController = FindObjectOfType<GameController>();
            gameController.showPanels.HideMenu();
        }
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
            GameController.Instance.OnPickUpGet(other.gameObject);
        }
    }

}