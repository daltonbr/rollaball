using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed;
    public Text countText;
    public Text winText;
	
	private Rigidbody rb;
    private int count;
	
	void Start ()
	{
        count = 0;
		rb = GetComponent<Rigidbody>();
        SetCountText();  //atualiza a contagem de pontos no UI
        winText.text = "";
    }
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Pick Up") )
		{
		    other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

    }

    void SetCountText()   //atualiza a contagem de pontos no UI
    {
        countText.text = "Count: " + count.ToString();
        if ( count >= 12 )
        {
            winText.text = "You Win!";
        }
    }

}