using UnityEngine;
using System.Collections;

public class MagnetPowerUp : MonoBehaviour {

    public LayerMask m_MagneticLayers;
    //public Vector3 m_Position;
    public float m_Radius;    // sugestao 3
    public float intensity;   // sugestao 60
    public float duration;    // duration of the PowerUp
    private Vector3 gravityPull;
    public ParticleSystem particlesPowerUp; //

    void FixedUpdate()
    {
        Collider[] colliders;  // vetor de colliders
        Rigidbody rigidbody;

        colliders = Physics.OverlapSphere(transform.position, m_Radius, m_MagneticLayers);  //captura os colliders dentro do raio, e que pertencem ao layer
        foreach (Collider collider in colliders)  //itera sobre todos os colliders
        {
            rigidbody = (Rigidbody)collider.gameObject.GetComponent(typeof(Rigidbody));
            gravityPull = transform.position - rigidbody.transform.position;  //vetor diferenca
            if (rigidbody == null)  // se nao tem collider associado...
            {
                continue;  // ...continua iterando
            }
            else
            {
                rigidbody.AddForce((gravityPull.normalized) * intensity * 1 / (Mathf.Pow((gravityPull.magnitude), 3.0F)));   // || diferenca ||*30 - modulo^-3  
                //Debug.Log(Mathf.Pow((gravityPull.magnitude), 3.0F));
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
