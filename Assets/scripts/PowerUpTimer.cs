using UnityEngine;
using System.Collections;

public class PowerUpTimer : MonoBehaviour {

    private float deltaTime;  //pega do script do PowerUp em questao
    public GameObject player;
    //public Component powerUpScript; // por enquanto nao funciona

    void OnTriggerEnter(Collider other) //algo colide com o PowerUp
    {
        if (other.gameObject.CompareTag("Player"))  //soh ativa o powerUp qdo o Player o pega
        {
            deltaTime = player.GetComponent<MagnetPowerUp>().duration;
            player.GetComponent<MagnetPowerUp>().enabled = true;  //habilita o Script Magnet no player
            this.GetComponent<MeshRenderer>().enabled = false;
            player.GetComponent<MagnetPowerUp>().particlesPowerUp.Play();
            StartCoroutine( TimeUp() );   
        }
    }

    IEnumerator TimeUp()  // to WaitForSeconds we need this function to be iEnumerator
    {
        yield return new WaitForSeconds(deltaTime);  // time that the PowerUp is active
        Debug.Log("time's up!");
        player.GetComponent<MagnetPowerUp>().particlesPowerUp.Stop();
        player.GetComponent<MagnetPowerUp>().enabled = false;  //desabilita o Script Magnet no player
        Destroy(this.gameObject);  //destroi o PowerUp
    }
}
