using UnityEngine;
using System.Collections;


public class Spinner : MonoBehaviour
{

    public float angularVelocity = 1;

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime * angularVelocity);
    }
}
