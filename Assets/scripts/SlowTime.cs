using UnityEngine;
using System.Collections;

public class SlowTime : MonoBehaviour {

    public float timeRate = 0.5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Time.timeScale = timeRate;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 1f;
        }
    }
}

