using UnityEngine;
using System.Collections;

public class PowerUpTimer : MonoBehaviour {

    public bool active;
    public float deltaTime;
    private float initialTime, finalTime;

    void OnTriggerEnter (Collider other)
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        Destroy(this.gameObject, deltaTime);  //destroy the powerUp, depois de um tempo
        active = true;
        initialTime = Time.time;  //pega o tempo inicial
        finalTime = deltaTime + initialTime;  // tempo que desliga o powerUp
        Debug.Log(finalTime);
    }
}
