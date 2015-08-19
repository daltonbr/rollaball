using UnityEngine;
using System.Collections;

public class CannonFire : MonoBehaviour {

    public Rigidbody projectile;
    public float bombSpeed = 10.0f;
    public float timeToExplode = 2.0f;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody clone;
            clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            clone.velocity = transform.TransformDirection(Vector3.forward * bombSpeed);
            Destroy(clone.gameObject, timeToExplode);
            clone.GetComponent<Explosion>().Boom(timeToExplode);
            //explosionScript.Boom();
            
        }
    }
}
