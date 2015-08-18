using UnityEngine;
using System.Collections;

// Applies an explosion force to all nearby rigidbodies
public class Explosion : MonoBehaviour
{
	public float radius;
	public float power;
    public Transform explosion;
	Collider[] colliders;
	Vector3 explosionPos;
	
	void OnTriggerEnter(Collider other) 
	{
		this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
		if (other.gameObject.CompareTag ("Player")) {

			Boom ();
			this.gameObject.SetActive(false);

		}
	}
	public void Boom(float timeToExplode)
    {
        Vector3 explosionPos = transform.position;
        //GameObject explosion = (GameObject)Instantiate(Resources.Load("MyPrefab")); ;

        StartCoroutine(WaitAndBoom(timeToExplode));  //wait

        Instantiate(explosion, explosionPos, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);
			
		foreach (Collider hit in colliders)
        {
			if (hit.attachedRigidbody != null)
				hit.attachedRigidbody.AddExplosionForce (power, explosionPos, radius,1.0f);
            //Debug.Log("Boom");
            Destroy(this.gameObject);
		}
    }

    IEnumerator WaitAndBoom(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //print("Waited " + Time.time);
    }

public void Boom()   // refazer esse codigo com o outro Boom() com timer
    {
        Vector3 explosionPos = transform.position;
        //GameObject explosion = (GameObject)Instantiate(Resources.Load("MyPrefab")); ;
        Instantiate(explosion, explosionPos, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        foreach (Collider hit in colliders)
        {
            if (hit.attachedRigidbody != null)
                hit.attachedRigidbody.AddExplosionForce(power, explosionPos, radius, 1.0f);
            //Debug.Log("Boom");
            Destroy(this.gameObject);
        }
    }
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, radius); // desenha uma esfera com o raio de contato para explosao, para fins de debug
    //}
}




