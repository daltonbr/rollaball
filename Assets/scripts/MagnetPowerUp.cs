using UnityEngine;
using System.Collections;

public class MagnetPowerUp : MonoBehaviour {

   // public float radius;
    //private Transform player;
   // private Transform pickup;
   // private Vector3 gravityPull;

    /*
    void OnTriggerStay (Collider other)
    {
        pickup = other.transform;
        player = this.transform;

        if (other.tag == "Pick Up")
        {
            gravityPull = player.transform.position - pickup.transform.position;  // vetor diferenca        
            other.transform.position = gravityPull;

            other.GetComponent<Rigidbody>().MovePosition((gravityPull) * 2.0f);
            Debug.Log(gravityPull);
        }
    }
    */
    public LayerMask m_MagneticLayers;
    //public Vector3 m_Position;
    public float m_Radius;
    public float intensity;
    private Vector3 gravityPull;

    void FixedUpdate()
    {
        Collider[] colliders;  // vetor de colliders
        Rigidbody rigidbody;

        colliders = Physics.OverlapSphere(transform.position, m_Radius, m_MagneticLayers);  //captura os colliders dentro do raio, e que pertencem ao layer
        foreach (Collider collider in colliders)  //itera sobre todos os colliders
        {
            rigidbody = (Rigidbody)collider.gameObject.GetComponent(typeof(Rigidbody));
            gravityPull = transform.position - rigidbody.transform.position;  //vetor diferenca
            if (rigidbody == null)  // se nao tem collider associoado...
            {
                continue;  // ...continua iterando
            }
            else
            {
                //Debug.Log(rigidbody.name);
                //rigidbody.AddExplosionForce(m_Force * -1, transform.position + m_Position, m_Radius);
                //rigidbody.AddForce( gravityPull.normalized * 1/(gravityPull.magnitude) );   // diferenca - modulo  (linear)
                rigidbody.AddForce((gravityPull.normalized) * intensity * 1 / (Mathf.Pow((gravityPull.magnitude), 3.0F)));   // || diferenca ||*30 - modulo^-3  
                //Debug.Log(Mathf.Pow((gravityPull.magnitude), 3.0F));
                //private float t;
                //Mathf.Lerp(gravityPull.normalized, gravityPull.magnitude, t)
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position + m_Position, m_Radius);
        Gizmos.DrawRay(transform.position, gravityPull);
    }
}
