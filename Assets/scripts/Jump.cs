using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

    public float intensity = 1;
    bool isGrounded = true;
    bool isDoubleJumped = false;
    Rigidbody playerRB;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Jump"))
        {
         
            if ( isGrounded || !isDoubleJumped )
            {
                DoJump();
            }
        }

    }
        void DoJump()
        {
            if (!isDoubleJumped)
            {
                playerRB.AddForce(transform.up * intensity);
                isDoubleJumped = true;
            }

            
            if (isGrounded)
            {
                playerRB.AddForce(transform.up * intensity);
                isGrounded = false;
            }
    }

}
